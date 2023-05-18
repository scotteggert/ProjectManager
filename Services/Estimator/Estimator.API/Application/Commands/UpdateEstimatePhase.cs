namespace Estimator.Application.Commands
{
    public class UpdateEstimatePhaseCommand : IRequest<bool>
    {
        public string JobCode { get; }
        public EstimatePhase Phase { get; }

        public UpdateEstimatePhaseCommand(string jobCode, EstimatePhase phase)
        {
            JobCode = jobCode;
            Phase = phase;
        }
    }
    
    
    
}
