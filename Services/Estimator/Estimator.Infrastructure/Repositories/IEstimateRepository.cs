namespace Estimator.Infrastructure.Repositories
{
    public interface IEstimateRepository
    {
        Task AddAsync(Estimate estimate);
        void Delete(Estimate estimate);
        Task<Estimate> GetAsync(int id);
        Task<IEnumerable<Estimate>> GetByClientNameAsync(string clientName);
        Task<IEnumerable<Estimate>> GetByProjectManagerAsync(string projectManager);
        Task<int> UnitOfWorkSaveChangesAsync();
        void Update(Estimate estimate);
    }
}