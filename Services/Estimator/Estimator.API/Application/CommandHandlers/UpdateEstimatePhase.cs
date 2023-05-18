namespace Estimator.Application.CommandHandlers
{
    public class UpdateEstimatePhaseCommandHandler : IRequestHandler<UpdateEstimatePhaseCommand, bool>
    {
        private readonly IEstimateRepository _estimateRepository;

        public UpdateEstimatePhaseCommandHandler(IEstimateRepository estimateRepository)
        {
            _estimateRepository = estimateRepository;
        }

        public async Task<bool> Handle(UpdateEstimatePhaseCommand request, CancellationToken cancellationToken)
        {
            var estimate = await _estimateRepository.GetAsync(request.JobCode);

            if (estimate == null) return false;

            estimate.UpdatePhase(request.Phase);
            _estimateRepository.Update(estimate);
            await _estimateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
