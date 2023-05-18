namespace Estimator.Infrastructure.EntityConfigurations
{
    public class EstimatePhaseEntityTypeConfiguration : IEntityTypeConfiguration<EstimatePhase>
    {
        public void Configure(EntityTypeBuilder<EstimatePhase> estimatePhaseConfiguration)
        {
            estimatePhaseConfiguration.ToTable("EstimatePhases", EstimatorContext.DEFAULT_SCHEMA);

            estimatePhaseConfiguration.HasKey(o => o.Id);

            estimatePhaseConfiguration.Ignore(b => b.DomainEvents);

            estimatePhaseConfiguration.Property(o => o.Id)
                .UseHiLo("estimatephaseseq", EstimatorContext.DEFAULT_SCHEMA);

            estimatePhaseConfiguration.Property<string>("PhaseName").IsRequired();
            estimatePhaseConfiguration.Property<int>("Hours").IsRequired();

            // This assumes you have a "Phase" in the same schema
            //estimatePhaseConfiguration.HasOne<EstimatePhase>()
            //    .WithMany()
            //    .HasForeignKey("PhaseId")
            //    .OnDelete(DeleteBehavior.Restrict);

            estimatePhaseConfiguration.Property<int>("EstimateId").IsRequired();

            // This sets up the relationship with the parent Estimate
            estimatePhaseConfiguration.HasOne<Estimate>()
                .WithMany(e => e.Phases)
                .HasForeignKey("EstimateId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
