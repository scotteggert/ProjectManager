namespace Estimator.Application.CommandHandlers
{
    public class AddEstimatePhaseCommandHandler : IRequestHandler<AddEstimatePhaseCommand, bool>
    {
        private readonly IEstimateRepository _estimateRepository;

        public AddEstimatePhaseCommandHandler(IEstimateRepository estimateRepository)
        {
            _estimateRepository = estimateRepository;
        }

        public async Task<bool> Handle(AddEstimatePhaseCommand request, CancellationToken cancellationToken)
        {
            var estimate = await _estimateRepository.GetAsync(request.JobCode);

            if (estimate == null) return false;

            estimate.AddPhase(request.Phase);
            _estimateRepository.Update(estimate);
            await _estimateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
