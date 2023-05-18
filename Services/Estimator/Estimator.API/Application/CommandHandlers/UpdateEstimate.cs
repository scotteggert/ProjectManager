namespace Estimator.Application.CommandHandlers
{

    public class UpdateEstimateCommandHandler : IRequestHandler<UpdateEstimateCommand, bool>
    {
        // Inject your repository here
        private readonly IEstimateRepository _estimateRepository;

        public UpdateEstimateCommandHandler(IEstimateRepository estimateRepository)
        {
            _estimateRepository = estimateRepository;
        }

        public async Task<bool> Handle(UpdateEstimateCommand request, CancellationToken cancellationToken)
        {
            var estimate = await _estimateRepository.GetAsync(request.JobCode);

            if (estimate == null) return false;

            // Call method on the estimate to update the rate card
            estimate.UpdateRateCard(request.RateCard);

            // Call repository method to update and persist the estimate
            _estimateRepository.Update(estimate);
            await _estimateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
