namespace Estimator.Infrastructure.EntityConfigurations
{
    public class EstimateEntityTypeConfiguration : IEntityTypeConfiguration<Estimate>
    {
        public void Configure(EntityTypeBuilder<Estimate> estimateConfiguration)
        {

            estimateConfiguration.ToTable("Estimates", EstimatorContext.DEFAULT_SCHEMA);

            estimateConfiguration.HasKey(o => o.Id);

            estimateConfiguration.Ignore(b => b.DomainEvents);

            estimateConfiguration.Property(o => o.Id)
                .UseHiLo("estimateseq", EstimatorContext.DEFAULT_SCHEMA);

            estimateConfiguration.Property<string>("ClientName").IsRequired();
            estimateConfiguration.Property<string>("JobCode").IsRequired();
            estimateConfiguration.Property<string>("ProjectManager").IsRequired();

            estimateConfiguration.Property<int>("RateCardId").IsRequired();

            // Assuming you have a "RateCard" in the same schema
            estimateConfiguration.HasOne<RateCard>()
                .WithMany()
                .HasForeignKey("RateCardId")
                .OnDelete(DeleteBehavior.Restrict);

            estimateConfiguration.Property<decimal>("_totalEstimate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("TotalEstimate")
                .IsRequired();

            var navigation = estimateConfiguration.Metadata.FindNavigation(nameof(Estimate.Phases));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }


}
