namespace Estimator.Domain.AggregatesModel.Estimate
{
    public class Estimate : Entity, IAggregateRoot
    {
        // Private fields
        private readonly string _clientName;
        private readonly string _jobCode;
        private readonly string _projectManager;
        private readonly SortedList<int, EstimatePhase> _phases = new SortedList<int, EstimatePhase>();
        private RateCard.RateCard _rateCard;
        private decimal _totalEstimate;

        // Constructors
        private Estimate() { }

        public Estimate(string clientName, string jobCode, string projectManager, RateCard.RateCard rateCard, SortedList<int, EstimatePhase> phases)
        {
            _clientName = clientName ?? throw new ArgumentNullException(nameof(clientName));
            _jobCode = jobCode ?? throw new ArgumentNullException(nameof(jobCode));
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _rateCard = rateCard ?? throw new ArgumentNullException(nameof(rateCard));
            _phases = phases ?? throw new ArgumentNullException(nameof(phases));
        }

        // Public properties
        public string ClientName => _clientName;
        public string JobCode => _jobCode;
        public string ProjectManager => _projectManager;
        public IReadOnlyList<EstimatePhase> Phases => _phases.Values.ToList<EstimatePhase>();
        public RateCard.RateCard RateCard => _rateCard;

        public decimal TotalEstimate
        {
            get => _totalEstimate;
            private set => _totalEstimate = value;
        }

        // Public methods
        public void AddPhase(EstimatePhase phase, int order)
        {
            _phases.Add(order, phase);
        }

        public void RemovePhase(EstimatePhase phase)
        {
            var order = _phases.FirstOrDefault(p => p.Value == phase).Key;
            _phases.Remove(order);

            AddDomainEvent(new EstimatePhaseRemoved(this, phase));
        }

        public void ChangePhaseOrder(EstimatePhase phase, int newOrder)
        {
            if (phase == null)
            {
                throw new ArgumentNullException(nameof(phase));
            }

            var currentOrder = _phases.FirstOrDefault(p => p.Value == phase).Key;
            if (currentOrder == 0)
            {
                throw new InvalidOperationException($"Phase '{phase.Name}' does not exist in the estimate.");
            }

            _phases.Remove(currentOrder);
            _phases.Add(newOrder, phase);

            // Publish domain event
            AddDomainEvent(new EstimatePhaseOrderChanged(phase, newOrder));
        }

        public void UpdateRateCard(RateCard.RateCard rateCard)
        {
            if (rateCard == null)
            {
                throw new ArgumentNullException(nameof(rateCard));
            }

            _rateCard = rateCard;

            AddDomainEvent(new RateCardUpdated(this, rateCard));
        }

     
        public void CalculateTotalEstimate()
        {
            _totalEstimate = _phases.Sum(phase => phase.Value.GetTotal());
        }
    }
}
    