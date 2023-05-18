namespace Estimator.Application.Models
{
    public class EstimatePhase
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public IEnumerable<EstimatePhaseRateCardItem> RateCardItems { get; init; }

        public EstimatePhase(int id, string name, string description, IEnumerable<EstimatePhaseRateCardItem> rateCardItems)
        {
            Id = id;
            Name = name;
            Description = description;
            RateCardItems = rateCardItems;
        }
    }
}
