namespace Estimator.Application.CommandHandlers
{
    public class UpdateRateCardCommandHandler : IRequestHandler<UpdateRateCardCommand, bool>
    {
        private readonly IRateCardRepository _rateCardRepository;

        public UpdateRateCardCommandHandler(IRateCardRepository rateCardRepository)
        {
            _rateCardRepository = rateCardRepository;
        }

        public async Task<bool> Handle(UpdateRateCardCommand request, CancellationToken cancellationToken)
        {
            _rateCardRepository.Update(request.RateCard);
            return await _rateCardRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
