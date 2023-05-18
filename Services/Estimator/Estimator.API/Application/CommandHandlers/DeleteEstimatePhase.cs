namespace Estimator.API.Application.CommandHandlers
{
    public class DeleteEstimatePhaseCommandHandler : IRequestHandler<DeleteEstimatePhaseCommand, bool>
    {
        private readonly IEstimateRepository _estimateRepository;

        public DeleteEstimatePhaseCommandHandler(IEstimateRepository estimateRepository)
        {
            _estimateRepository = estimateRepository;
        }

        public async Task<bool> Handle(DeleteEstimatePhaseCommand request, CancellationToken cancellationToken)
        {
            var estimate = await _estimateRepository.GetAsync(request.JobCode);

            if (estimate == null) return false;

            estimate.DeletePhase(request.PhaseId);
            _estimateRepository.Update(estimate);
            await _estimateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
