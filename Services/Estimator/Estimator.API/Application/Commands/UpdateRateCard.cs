namespace Estimator.Application.Commands
{
    public class UpdateRateCardCommand : IRequest<bool>
    {
        public RateCard RateCard { get; }

        public UpdateRateCardCommand(RateCard rateCard)
        {
            RateCard = rateCard;
        }
    }
}
