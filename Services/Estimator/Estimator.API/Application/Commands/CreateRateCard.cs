namespace Estimator.Application.Commands
{
    public class CreateRateCardCommand : IRequest<bool>
    {
        public RateCard RateCard { get; }

        public CreateRateCardCommand(RateCard rateCard)
        {
            RateCard = rateCard;
        }
    }
}
