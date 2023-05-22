namespace Estimator.UI.Model
{
    public class RateCardItem
    {
        public int Id { get; set; }

        public Guid RateCardItemId { get;  set; }
        public string RoleName { get;  set; }
        public string GroupName { get;  set; }
        public decimal Rate { get;  set; }
        public DateTime EffectiveFrom { get;  set; }
        public DateTime? EffectiveTo { get;  set; }
        
        public int Order { get; set; }

        public RateCardItem() { }

        public RateCardItem(int id, Guid rateCardItemId, string roleName, string groupName, decimal rate, DateTime effectiveFrom, DateTime? effectiveTo)
        {
            Id = id;
            RateCardItemId = rateCardItemId;
            RoleName = roleName;
            GroupName = groupName;
            Rate = rate;
            EffectiveFrom = effectiveFrom;
            EffectiveTo = effectiveTo;
        }
    }

}
   