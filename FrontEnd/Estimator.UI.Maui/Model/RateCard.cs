namespace Estimator.UI.Model
{
    public class RateCard
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public List<RateCardItem> RateCardItems { get; init; }

        public RateCard() { }

        public RateCard(string id, string name, List<RateCardItem> rateCardItems)
        {
            Id = id;
            Name = name;
            RateCardItems = rateCardItems;
        }
    }
}
