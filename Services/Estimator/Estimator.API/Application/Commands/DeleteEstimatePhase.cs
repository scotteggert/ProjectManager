namespace Estimator.Application.Commands
{
    public class DeleteEstimatePhaseCommand : IRequest<bool>
    {
        public string JobCode { get; }
        public int PhaseId { get; }

        public DeleteEstimatePhaseCommand(string jobCode, int phaseId)
        {
            JobCode = jobCode;
            PhaseId = phaseId;
        }
    }
}
