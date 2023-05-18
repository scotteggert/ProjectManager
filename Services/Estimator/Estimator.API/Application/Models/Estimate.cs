namespace Estimator.Application.Models
{
    public class Estimate
    {
        public string ClientName { get; set; }
        public string JobCode { get; set; }
        public string ProjectManager { get; set; }
        public RateCard RateCard { get; set; }
        public List<EstimatePhase> EstimatePhases { get; set; }

        public Estimate(string clientName, string jobCode, string projectManager, RateCard rateCard, List<EstimatePhase> estimatePhases)
        {
            ClientName = clientName;
            JobCode = jobCode;
            ProjectManager = projectManager;
            RateCard = rateCard;
            EstimatePhases = estimatePhases;
        }
    }
}
