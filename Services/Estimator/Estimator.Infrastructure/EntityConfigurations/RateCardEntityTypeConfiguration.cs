namespace Estimator.Infrastructure.EntityConfigurations
{
    public class RateCardEntityTypeConfiguration : IEntityTypeConfiguration<RateCard>
    {
        public void Configure(EntityTypeBuilder<RateCard> rateCardConfiguration)
        {
            rateCardConfiguration.HasKey(rc => rc.Id);
            rateCardConfiguration.Ignore(rc => rc.DomainEvents);

            rateCardConfiguration.Property(rc => rc.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the relationship with RateCardItem
            var navigation = rateCardConfiguration.Metadata.FindNavigation(nameof(RateCard.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            rateCardConfiguration.HasMany(rc => rc.Items)
                .WithOne()
                .HasForeignKey("RateCardId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
