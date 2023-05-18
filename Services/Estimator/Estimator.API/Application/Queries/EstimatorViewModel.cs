namespace Estimator.Application.Queries
{
    public record Estimate
    {
        public Guid EstimateId { get; init; }
        public string ClientName { get; init; }
        public string JobCode { get; init; }
        public string ProjectManager { get; init; }
        public decimal TotalEstimate { get; init; }
        public RateCard RateCard { get; init; }
        public List<EstimatePhase> EstimatePhases { get; set; }
    }

    public record EstimatePhase
    {
        public Guid EstimatePhaseId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<EstimatePhaseRateCardItem> EstimatePhaseRateCardItems { get; set; }
    }

    public record EstimatePhaseRateCardItem
    {
        public Guid EstimatePhaseRateCardItemId { get; init; }
        public Guid RateCardItemId { get; init; }
        public string RoleName { get; init; }
        public decimal Rate { get; init; }
        public int Count { get; init; }
    }
    
    public record RateCard
    {
        public Guid RateCardId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<RateCardItem> RateCardItems { get; init; }     
    }

    public record RateCardItem
    {
        public Guid RateCardItemId { get; init; }
        public string RoleName { get; init; }
        public decimal Rate { get; init; }
        public DateTime EffectiveFrom { get; init; }
        public DateTime? EffectiveTo { get; init; }
    }
}
