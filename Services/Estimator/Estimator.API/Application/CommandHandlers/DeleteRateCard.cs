namespace Estimator.Application.CommandHandlers
{
    public class DeleteRateCardCommandHandler : IRequestHandler<DeleteRateCardCommand, bool>
    {
        private readonly IRateCardRepository _rateCardRepository;

        public DeleteRateCardCommandHandler(IRateCardRepository rateCardRepository)
        {
            _rateCardRepository = rateCardRepository;
        }

        public async Task<bool> Handle(DeleteRateCardCommand request, CancellationToken cancellationToken)
        {
            var rateCard = await _rateCardRepository.GetAsync(request.RateCardId);

            if (rateCard == null) return false;

            _rateCardRepository.Delete(rateCard);
            await _rateCardRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
