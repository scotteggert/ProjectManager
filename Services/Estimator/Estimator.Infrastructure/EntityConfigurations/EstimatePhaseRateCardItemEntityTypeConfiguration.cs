namespace Estimator.Infrastructure.EntityConfigurations
{
    public class EstimatePhaseRateCardItemEntityTypeConfiguration : IEntityTypeConfiguration<EstimatePhaseRateCardItem>
    {
        public void Configure(EntityTypeBuilder<EstimatePhaseRateCardItem> estimatePhaseRateCardItemConfiguration)
        {
            estimatePhaseRateCardItemConfiguration.ToTable("EstimatePhaseRateCardItems", EstimatorContext.DEFAULT_SCHEMA);

            estimatePhaseRateCardItemConfiguration.HasKey(o => o.Id);

            estimatePhaseRateCardItemConfiguration.Ignore(b => b.DomainEvents);

            estimatePhaseRateCardItemConfiguration.Property(o => o.Id)
                .UseHiLo("estimatephaseratecarditemseq", EstimatorContext.DEFAULT_SCHEMA);

            estimatePhaseRateCardItemConfiguration.Property<int>("EstimatePhaseId").IsRequired();

            // Set up the relationship with the parent EstimatePhase
            estimatePhaseRateCardItemConfiguration.HasOne<EstimatePhase>()
                .WithMany(e => e.RateCardItems)
                .HasForeignKey("EstimatePhaseId")
                .OnDelete(DeleteBehavior.Cascade);

            estimatePhaseRateCardItemConfiguration.Property<int>("RateCardItemId").IsRequired();

            // This assumes you have a "RateCardItem" in the same schema
            estimatePhaseRateCardItemConfiguration.HasOne<RateCardItem>()
                .WithMany()
                .HasForeignKey("RateCardItemId")
                .OnDelete(DeleteBehavior.Restrict);

            estimatePhaseRateCardItemConfiguration.Property<int>("Quantity").IsRequired();
            estimatePhaseRateCardItemConfiguration.Property<decimal>("Rate").IsRequired();
        }
    }
}
