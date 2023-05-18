namespace Estimator.Domain.AggregatesModel.RateCard
{
     public class RateCardItem : Entity
    {
        public Guid RateCardItemId { get; private set; }
        public string RoleName { get; private set; }
        public decimal Rate { get; private set; }
        public DateTime EffectiveFrom { get; private set; }
        public DateTime? EffectiveTo { get; private set; }

        // EF Core needs a parameterless constructor
        protected RateCardItem() { }

        public RateCardItem(Guid ratecarditemid, string roleName, decimal rate, DateTime effectiveFrom, DateTime? effectiveTo = null)
        {
            RateCardItemId = ratecarditemid;
            RoleName = roleName;
            Rate = rate;
            EffectiveFrom = effectiveFrom;
            EffectiveTo = effectiveTo;
        }
    }
}
