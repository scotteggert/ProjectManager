namespace Estimator.Infrastructure.Repositories
{
    public interface IRateCardRepository
    {
        Task AddAsync(RateCard rateCard);
        void Delete(RateCard rateCard);
        Task<RateCard> GetAsync(int id);
        Task<IEnumerable<RateCard>> GetAllAsync();
        Task<int> UnitOfWorkSaveChangesAsync();
        void Update(RateCard rateCard);
    }
}
