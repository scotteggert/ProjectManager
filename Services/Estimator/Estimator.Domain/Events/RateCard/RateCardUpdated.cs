namespace Estimator.Domain.Events
{
    public class RateCardUpdated : INotification
    {
        public RateCardUpdated(Estimate estimate, RateCard rateCard)
        {
            Estimate = estimate ?? throw new ArgumentNullException(nameof(estimate));
            RateCard = rateCard ?? throw new ArgumentNullException(nameof(rateCard));
        }

        public Estimate Estimate { get; }
        public RateCard RateCard { get; }
    }
}
