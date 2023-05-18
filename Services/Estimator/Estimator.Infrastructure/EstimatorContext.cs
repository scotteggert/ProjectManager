namespace Estimator.Infrastructure;

public class EstimatorContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "estimator";
    public DbSet<Estimate> Estimates { get; set; }
    public DbSet<EstimatePhase> EstimatePhases { get; set; }
    public DbSet<EstimatePhaseRateCardItem> EstimatePhaseRateCardItems { get; set; }
    public DbSet<RateCard> RateCards { get; set; }
    public DbSet<RateCardItem> RateCardItems { get; set; }

    private readonly IMediator _mediator;
    private IDbContextTransaction _currentTransaction;

    public EstimatorContext(DbContextOptions<EstimatorContext> options) : base(options) { }

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public EstimatorContext(DbContextOptions<EstimatorContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("EstimatorContext::ctor ->" + this.GetHashCode());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EstimateEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EstimatePhaseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EstimatePhaseRateCardItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RateCardEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RateCardItemEntityTypeConfiguration());

    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}

public class EstimatorContextDesignFactory : IDesignTimeDbContextFactory<EstimatorContext>
{
    public EstimatorContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EstimatorContext>()
            .UseSqlServer("Server=.;Initial Catalog=EstimatorDb;Integrated Security=true");

        return new EstimatorContext(optionsBuilder.Options, new NoMediator());
    }

    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return default(IAsyncEnumerable<TResponse>);
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return default(IAsyncEnumerable<object?>);
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<TResponse>(default(TResponse));
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(object));
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            return Task.CompletedTask;
        }
    }
}
