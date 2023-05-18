namespace Estimator.Application.Commands
{
    public class AddEstimatePhaseCommand : IRequest<bool>
    {
        public string JobCode { get; }
        public EstimatePhase Phase { get; }

        public AddEstimatePhaseCommand(string jobCode, EstimatePhase phase)
        {
            JobCode = jobCode;
            Phase = phase;
        }
    }
}
