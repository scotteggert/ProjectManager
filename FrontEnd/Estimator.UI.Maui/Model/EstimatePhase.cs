namespace Estimator.UI.Model
{
    public class EstimatePhase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EstimatePhaseRateCardItem> EstimatePhaseRateCardItems { get; set; }
        public DateTime EstimatedStartDate { get; set; } = DateTime.UtcNow;
        public DateTime EstimatedEndDate { get; set; } = DateTime.UtcNow.AddDays(3);

        public EstimatePhase() {
            EstimatePhaseRateCardItems = new List<EstimatePhaseRateCardItem>();
        }

        public EstimatePhase(int id, string name, string description, List<EstimatePhaseRateCardItem> rateCardItems, DateTime estimatedStartDate, DateTime estimatedEndDate)
        {
            Id = id;
            Name = name;
            Description = description;
            EstimatePhaseRateCardItems = rateCardItems;
            EstimatedStartDate = estimatedStartDate;
            EstimatedEndDate = estimatedEndDate;    

        }
    }
}