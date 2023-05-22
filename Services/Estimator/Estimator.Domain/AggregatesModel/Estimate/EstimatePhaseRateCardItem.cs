namespace Estimator.Domain.AggregatesModel.Estimate
{
    public class EstimatePhaseRateCardItem : Entity
    {
        public Guid EstimatePhaseId { get; set; }
        public EstimatePhase EstimatePhase { get; set; }
        public Guid RateCardItemId { get; set; }
        public RateCardItem RateCardItem { get; set; } //
        public int Count { get; set; }
        public int PercentageUtilized { get; set; }
    }
}
