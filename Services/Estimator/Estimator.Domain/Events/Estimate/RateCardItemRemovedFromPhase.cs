namespace Estimator.Domain.Events
{
    public class RateCardItemRemovedFromPhase : INotification
    {
        public EstimatePhase EstimatePhase { get; }
        public RateCardItem RateCardItem { get; }

        public RateCardItemRemovedFromPhase(EstimatePhase estimatePhase, RateCardItem rateCardItem)
        {
            EstimatePhase = estimatePhase ?? throw new ArgumentNullException(nameof(estimatePhase));
            RateCardItem = rateCardItem ?? throw new ArgumentNullException(nameof(rateCardItem));
        }
    }
}
