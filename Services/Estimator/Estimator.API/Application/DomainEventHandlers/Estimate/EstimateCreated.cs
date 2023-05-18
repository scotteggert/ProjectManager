namespace Estimator.API.Application.DomainEventHandlers.Estimate
{
    public class EstimateCreatedDomainEventHandler : INotificationHandler<EstimateCreatedDomainEvent>
    {
        public async Task Handle(EstimateCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Implement handling logic
        }
    }
}
