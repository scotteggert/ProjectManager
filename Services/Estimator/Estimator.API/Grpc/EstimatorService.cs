namespace GrpcEstimator
{
    public class EstimatorService : EstimatorGrpc.EstimatorGrpcBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EstimatorService> _logger;

        public EstimatorService(IMediator mediator, ILogger<EstimatorService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

    }
}
