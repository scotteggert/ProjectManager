namespace Estimator.Application.Models
{
    public class RateCardItem
    {
        public int Id { get; set; }

        public Guid RateCardItemId { get; private set; }
        public string RoleName { get; private set; }
        public decimal Rate { get; private set; }
        public DateTime EffectiveFrom { get; private set; }
        public DateTime? EffectiveTo { get; private set; }



    }
}
