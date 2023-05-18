namespace Estimator.Application.Commands
{
    public class UpdateEstimateCommand : IRequest<bool>
    {
        public string JobCode { get; }
        public RateCard RateCard { get; }

        public UpdateEstimateCommand(string jobCode, RateCard rateCard)
        {
            JobCode = jobCode;
            RateCard = rateCard;
        }
    }
}
