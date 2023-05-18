namespace Estimator.Application.IntegrationEvents
{
    public interface IEstimatorIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
