namespace Estimator.API.Infrastructure.Factories
{
    public class EstimatorDbContextFactory : IDesignTimeDbContextFactory<EstimatorContext>
    {
        public EstimatorContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EstimatorContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Ordering.API"));

            return new EstimatorContext(optionsBuilder.Options);
        }
    }
}
