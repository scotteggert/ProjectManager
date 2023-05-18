namespace Estimator.Domain.Events
{
    public class RateCardCreated : INotification
    {
        public RateCardCreated(RateCard rateCard)
        {
            RateCard = rateCard ?? throw new ArgumentNullException(nameof(rateCard));
        }

        public RateCard RateCard { get; }
    }
}
