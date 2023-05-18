namespace Estimator.Domain.Events
{
    public class RateCardItemRemoved : INotification
    {
        public RateCardItemRemoved(RateCard rateCard, RateCardItem item)
        {
            RateCard = rateCard ?? throw new ArgumentNullException(nameof(rateCard));
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public RateCard RateCard { get; }
        public RateCardItem Item { get; }
    }
}