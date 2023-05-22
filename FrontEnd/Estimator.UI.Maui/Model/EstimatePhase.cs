namespace Estimator.UI.Model
{
    public class EstimatePhase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EstimatePhaseRateCardItem> RateCardItems { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }

        public EstimatePhase() {
            RateCardItems = new List<EstimatePhaseRateCardItem>();
        }

        public EstimatePhase(int id, string name, string description, List<EstimatePhaseRateCardItem> rateCardItems, DateTime estimatedStartDate, DateTime estimatedEndDate)
        {
            Id = id;
            Name = name;
            Description = description;
            RateCardItems = rateCardItems;
            EstimatedStartDate = estimatedStartDate;
            EstimatedEndDate = estimatedEndDate;    

        }
    }
}