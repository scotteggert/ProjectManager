namespace Estimator.Application.CommandHandlers
{
    public class CreateRateCardCommandHandler : IRequestHandler<CreateRateCardCommand, bool>
    {
        private readonly IRateCardRepository _rateCardRepository;

        public CreateRateCardCommandHandler(IRateCardRepository rateCardRepository)
        {
            _rateCardRepository = rateCardRepository;
        }

        public async Task<bool> Handle(CreateRateCardCommand request, CancellationToken cancellationToken)
        {
            _rateCardRepository.Add(request.RateCard);
            return await _rateCardRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
