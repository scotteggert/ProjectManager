namespace Estimator.Domain.Events
{
    public class RateCardItemCountUpdatedInPhase : INotification
    {
        public EstimatePhase EstimatePhase { get; }
        public RateCardItem RateCardItem { get; }
        public int Count { get; }

        public RateCardItemCountUpdatedInPhase(EstimatePhase estimatePhase, RateCardItem rateCardItem, int count)
        {
            EstimatePhase = estimatePhase ?? throw new ArgumentNullException(nameof(estimatePhase));
            RateCardItem = rateCardItem ?? throw new ArgumentNullException(nameof(rateCardItem));
            Count = count >= 1 ? count : throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than or equal to 1");
        }
    }
}
