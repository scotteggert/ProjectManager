namespace Estimator.Domain.AggregatesModel.Estimate
{
    public class EstimatePhase : Entity
    {
        private readonly string _name;
        private readonly string _description;
        private List<EstimatePhaseRateCardItem> _rateCardItems = new List<EstimatePhaseRateCardItem>();

        public EstimatePhase(string name, string description)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public IReadOnlyList<EstimatePhaseRateCardItem> RateCardItems => _rateCardItems.AsReadOnly();
        public string Name => _name;
        public string Description => _description;

        public void AddOrUpdateRateCardItem(RateCardItem rateCardItem, int count = 1)
        {
            if (rateCardItem == null)
            {
                throw new ArgumentNullException(nameof(rateCardItem));
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than or equal to 1");
            }

            var existingItem = _rateCardItems.Find(item => item.RateCardItemId == rateCardItem.RateCardItemId);

            if (existingItem != null)
            {
                existingItem.Count += count;
                AddDomainEvent(new RateCardItemCountUpdatedInPhase(this, rateCardItem, existingItem.Count));
            }
            else
            {
                var newItem = new EstimatePhaseRateCardItem { RateCardItemId = rateCardItem.RateCardItemId, Count = count };
                _rateCardItems.Add(newItem);
                AddDomainEvent(new RateCardItemAddedToPhase(this, rateCardItem, count));
            }
        }

        public void RemoveRateCardItem(RateCardItem rateCardItem, int count = 1)
        {
            if (rateCardItem == null)
            {
                throw new ArgumentNullException(nameof(rateCardItem));
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than or equal to 1");
            }

            var existingItem = _rateCardItems.Find(item => item.RateCardItemId == rateCardItem.RateCardItemId);

            if (existingItem == null)
            {
                throw new InvalidOperationException($"RateCardItem '{rateCardItem.RoleName}' not found in the estimate phase.");
            }

            existingItem.Count -= count;
            if (existingItem.Count <= 0)
            {
                _rateCardItems.Remove(existingItem);
                AddDomainEvent(new RateCardItemRemovedFromPhase(this, rateCardItem));
            }
            else
            {
                AddDomainEvent(new RateCardItemCountUpdatedInPhase(this, rateCardItem, existingItem.Count));
            }
        }

        public decimal GetTotal()
        {
            // Assuming RateCardItem has a property named Rate
            // This method now requires fetching related RateCardItem to calculate the total.
            // Consider refactoring your design if this results in inefficient database queries.
            return _rateCardItems.Sum(item => item.RateCardItem.Rate * item.Count);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (EstimatePhase)obj;
            return Id.Equals(other.Id);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
