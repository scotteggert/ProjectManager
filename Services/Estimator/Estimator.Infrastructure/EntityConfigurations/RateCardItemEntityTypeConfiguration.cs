namespace Estimator.Infrastructure.EntityConfigurations
{
    public class RateCardItemEntityTypeConfiguration : IEntityTypeConfiguration<RateCardItem>
    {
        public void Configure(EntityTypeBuilder<RateCardItem> rateCardItemConfiguration)
        {
            rateCardItemConfiguration.HasKey(rci => rci.Id);

            rateCardItemConfiguration.Property(rci => rci.RateCardItemId)
                .IsRequired();

            rateCardItemConfiguration.Property(rci => rci.RoleName)
                .IsRequired()
                .HasMaxLength(100);

            rateCardItemConfiguration.Property(rci => rci.Rate)
                .IsRequired();

            rateCardItemConfiguration.Property(rci => rci.EffectiveFrom)
                .IsRequired();

            rateCardItemConfiguration.Property(rci => rci.EffectiveTo);
        }
    }
}
