namespace Estimator.Application.CommandHandlers
{
    public class CreateEstimateCommandHandler : IRequestHandler<CreateEstimateCommand, bool>
    {
        // Inject your repository here
        private readonly IEstimateRepository _estimateRepository;

        public CreateEstimateCommandHandler(IEstimateRepository estimateRepository)
        {
            _estimateRepository = estimateRepository;
        }

        public async Task<bool> Handle(CreateEstimateCommand request, CancellationToken cancellationToken)
        {
            var estimate = new Estimate(request.ClientName, request.JobCode, request.ProjectManager, request.RateCard);

            // Call repository method to add and persist the new estimate
            _estimateRepository.Add(estimate);
            await _estimateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
