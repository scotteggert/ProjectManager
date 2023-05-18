namespace Estimator.Application.Commands
{
    public class DeleteEstimateCommand : IRequest<bool>
    {
        public string JobCode { get; }

        public DeleteEstimateCommand(string jobCode)
        {
            JobCode = jobCode;
        }
    }

}
