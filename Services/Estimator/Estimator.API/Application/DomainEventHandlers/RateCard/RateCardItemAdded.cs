namespace Estimator.API.Application.DomainEventHandlers.RateCard
{
    public class RateCardItemAddedDomainEventHandler : INotificationHandler<RateCardItemAdded>
    {
        public async Task Handle(RateCardItemAdded notification, CancellationToken cancellationToken)
        {
            // TODO: Implement handling logic
        }
    }
}
