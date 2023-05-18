namespace Estimator.API.Application.DomainEventHandlers.Estimate
{

    public class EstimatePhaseAddedDomainEventHandler : INotificationHandler<EstimatePhaseAddedDomainEvent>
    {
        public async Task Handle(EstimatePhaseAdded notification, CancellationToken cancellationToken)
        {
            // TODO: Implement handling logic
        }
    }
}
