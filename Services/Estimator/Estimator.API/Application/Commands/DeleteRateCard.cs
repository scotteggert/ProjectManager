namespace Estimator.Application.Commands
{
    public class DeleteRateCardCommand : IRequest<bool>
    {
        public int RateCardId { get; }

        public DeleteRateCardCommand(int rateCardId)
        {
            RateCardId = rateCardId;
        }
    }
}
