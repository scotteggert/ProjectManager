namespace Estimator.Domain.Events
{
    public class EstimateCreatedDomainEvent : INotification
    {
        public EstimateCreatedDomainEvent(Estimate estimate)
        {
            Estimate = estimate ?? throw new ArgumentNullException(nameof(estimate));
        }

        public Estimate Estimate { get; }
    }
}
