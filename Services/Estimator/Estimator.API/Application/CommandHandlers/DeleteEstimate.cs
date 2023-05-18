namespace Estimator.Application.CommandHandlers
{
    public class DeleteEstimateCommandHandler : IRequestHandler<DeleteEstimateCommand, bool>
    {
        private readonly IEstimateRepository _estimateRepository;

        public DeleteEstimateCommandHandler(IEstimateRepository estimateRepository)
        {
            _estimateRepository = estimateRepository;
        }

        public async Task<bool> Handle(DeleteEstimateCommand request, CancellationToken cancellationToken)
        {
            var estimate = await _estimateRepository.GetAsync(request.JobCode);

            if (estimate == null) return false;

            _estimateRepository.Delete(estimate);
            await _estimateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
