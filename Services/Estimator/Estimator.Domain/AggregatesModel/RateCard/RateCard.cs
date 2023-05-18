namespace Estimator.Domain.AggregatesModel.RateCard
{
    public class RateCard : Entity, IAggregateRoot
    {
        // Private fields
        private readonly string _name;
        private readonly List<RateCardItem> _items = new List<RateCardItem>();

        // Constructors
        private RateCard() { }

        public RateCard(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));

            // Raise domain event
            AddDomainEvent(new RateCardCreated(this));
        }

        // Public properties
        public string Name => _name;
        public IReadOnlyList<RateCardItem> Items => _items;

        // Public methods
        public void AddItem(string roleName, decimal rate, DateTime effectiveFrom, DateTime? effectiveTo = null)
        {
            ValidateRoleNameIsUnique(roleName);

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty", nameof(roleName));
            }

            if (rate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rate), "Rate cannot be negative");
            }
            if (effectiveTo.HasValue && effectiveFrom > effectiveTo.Value)
            {
                throw new ArgumentException("Effective From date cannot be later than Effective To date");
            }
            var item = new RateCardItem(Guid.NewGuid(), roleName, rate, effectiveFrom, effectiveTo);
            _items.Add(item);

            // Publish domain event
            AddDomainEvent(new RateCardItemAdded(this, item));
        }
        public void RemoveItem(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty", nameof(roleName));
            }

            var itemToRemove = _items.FirstOrDefault(item => item.RoleName == roleName);
            if (itemToRemove == null)
            {
                throw new InvalidOperationException($"Role '{roleName}' not found in the rate card.");
            }

            _items.Remove(itemToRemove);

            // Publish domain event
            AddDomainEvent(new RateCardItemRemoved(this, itemToRemove));
        }
        private void ValidateRoleNameIsUnique(string roleName)
        {
            if (_items.Any(item => item.RoleName == roleName))
            {
                throw new InvalidOperationException($"Role '{roleName}' already exists in the rate card.");
            }
        }
    }
}