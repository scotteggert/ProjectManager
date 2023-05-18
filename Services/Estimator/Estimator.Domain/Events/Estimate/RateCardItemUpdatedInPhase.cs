namespace Estimator.Domain.Events
{
    public class RateCardItemUpdatedInPhase : INotification
    {
        public RateCardItemUpdatedInPhase(EstimatePhase phase, RateCardItem item)
        {
            Phase = phase ?? throw new ArgumentNullException(nameof(phase));
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public EstimatePhase Phase { get; }
        public RateCardItem Item { get; }
    }
}
