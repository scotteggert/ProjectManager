namespace Estimator.Infrastructure.Repositories
{
    public class EstimateRepository : IEstimateRepository
    {
        private readonly EstimatorContext _context;

        public EstimateRepository(EstimatorContext context)
        {
            _context = context;
        }

        public async Task<Estimate> GetAsync(int id)
        {
            // We're including the rate card and all the phases for the estimate.
            return await _context.Estimates
                .Include(e => e.RateCard)
                .Include(e => e.Phases)
                .FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task<IEnumerable<Estimate>> GetByClientNameAsync(string clientName)
        {
            // Here we're getting all estimates for a particular client.
            return await _context.Estimates
                .Include(e => e.RateCard)
                .Include(e => e.Phases)
                .Where(e => e.ClientName == clientName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Estimate>> GetByProjectManagerAsync(string projectManager)
        {
            // Here we're getting all estimates managed by a particular project manager.
            return await _context.Estimates
                .Include(e => e.RateCard)
                .Include(e => e.Phases)
                .Where(e => e.ProjectManager == projectManager)
                .ToListAsync();
        }

        public async Task AddAsync(Estimate estimate)
        {
            await _context.Estimates.AddAsync(estimate);
        }

        public void Update(Estimate estimate)
        {
            _context.Estimates.Update(estimate);
        }

        public async Task<int> UnitOfWorkSaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Delete(Estimate estimate)
        {
            _context.Estimates.Remove(estimate);
        }

     

    }
}
