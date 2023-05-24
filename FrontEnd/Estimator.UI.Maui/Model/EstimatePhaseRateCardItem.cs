namespace Estimator.UI.Model
{
    public class EstimatePhaseRateCardItem
    {
        //public Guid EstimatePhaseRateCardItemId { get; init; }
        //public Guid EstimatePhaseId { get; init; }
        //public EstimatePhase EstimatePhase { get; init; }
        //public Guid RateCardItemId { get; init; }
        public RateCardItem RateCardItem { get; set; } //
        public int Count { get; set; }
        public int PercentageUtilized { get; set; }
    }
}
