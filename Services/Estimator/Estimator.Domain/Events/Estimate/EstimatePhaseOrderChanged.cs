namespace Estimator.Domain.Events
{
    public class EstimatePhaseOrderChanged : INotification
    {
        public EstimatePhaseOrderChanged(EstimatePhase phase, int newOrder)
        {
            Phase = phase ?? throw new ArgumentNullException(nameof(phase));
            NewOrder = newOrder;
        }

        public EstimatePhase Phase { get; }
        public int NewOrder { get; }
    }
}
