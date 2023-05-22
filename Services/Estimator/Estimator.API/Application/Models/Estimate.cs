namespace Estimator.Application.Models
{
    public class Estimate
    {
        public string ClientName { get; set; }
        public string JobCode { get; set; }
        public string ProjectManager { get; set; }
        public RateCard RateCard { get; set; }
        public List<EstimatePhase> EstimatePhases { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }


        public Estimate(string clientName, string jobCode, string projectManager, RateCard rateCard, List<EstimatePhase> estimatePhases, DateTime createdDate, DateTime estimatedStartDate, DateTime estimatedEndDate)
        {
            ClientName = clientName;
            JobCode = jobCode;
            ProjectManager = projectManager;
            RateCard = rateCard;
            EstimatePhases = estimatePhases;
            CreatedDate = createdDate;
            EstimatedStartDate = estimatedStartDate;
            EstimatedEndDate = estimatedEndDate;
        }
    }
}
