namespace Estimator.Application.Commands
{
    public class CreateEstimateCommand : IRequest<bool>
    {
        public string ClientName { get; }
        public string JobCode { get; }
        public string ProjectManager { get; }
        public RateCard RateCard { get; } // you may need to import the namespace for RateCard

        public CreateEstimateCommand(string clientName, string jobCode, string projectManager, RateCard rateCard)
        {
            ClientName = clientName;
            JobCode = jobCode;
            ProjectManager = projectManager;
            RateCard = rateCard;
        }
    }
}
