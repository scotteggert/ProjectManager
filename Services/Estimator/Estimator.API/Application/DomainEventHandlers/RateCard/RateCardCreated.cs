namespace Estimator.API.Application.DomainEventHandlers.RateCard
{
    public class RateCardCreatedDomainEventHandler : INotificationHandler<RateCardCreated>
    {
        public async Task Handle(RateCardCreated notification, CancellationToken cancellationToken)
        {
            // TODO: Implement handling logic
        }
    }

}
