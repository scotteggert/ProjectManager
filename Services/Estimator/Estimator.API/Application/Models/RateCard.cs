namespace Estimator.Application.Models
{
    public class RateCard
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<RateCardItem> RateCardItems { get; init; }

        public RateCard(string id, string name, IEnumerable<RateCardItem> rateCardItems)
        {
            Id = id;
            Name = name;
            RateCardItems = rateCardItems;
        }
    }
}
