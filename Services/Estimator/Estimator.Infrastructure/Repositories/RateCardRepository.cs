namespace Estimator.Infrastructure.Repositories
{
    public class RateCardRepository : IRateCardRepository
    {
        private readonly EstimatorContext _context;

        public RateCardRepository(EstimatorContext context)
        {
            _context = context;
        }

        public async Task<RateCard> GetAsync(int id)
        {
            // We're including all the items for the rate card.
            return await _context.RateCards
            .Include(rc => rc.Items)
            .FirstOrDefaultAsync(rc => rc.Id == id);
        }

        public async Task<IEnumerable<RateCard>> GetAllAsync()
        {
            // Here we're getting all rate cards.
            return await _context.RateCards
            .Include(rc => rc.Items)
            .ToListAsync();
        }

        public async Task AddAsync(RateCard rateCard)
        {
            await _context.RateCards.AddAsync(rateCard);
        }

        public void Update(RateCard rateCard)
        {
            _context.RateCards.Update(rateCard);
        }

        public async Task<int> UnitOfWorkSaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Delete(RateCard rateCard)
        {
            _context.RateCards.Remove(rateCard);
        }

    }
}