namespace Estimator.Domain.Events
{
    public class EstimatePhaseRemoved : INotification
    {
        public EstimatePhaseRemoved(Estimate estimate, EstimatePhase phase)
        {
            Estimate = estimate ?? throw new ArgumentNullException(nameof(estimate));
            Phase = phase ?? throw new ArgumentNullException(nameof(phase));
        }

        public Estimate Estimate { get; }
        public EstimatePhase Phase { get; }
    }
}