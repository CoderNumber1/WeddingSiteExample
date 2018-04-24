using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class BannerMap : EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Banner");
            this.Property(t => t.BannerId).HasColumnName("BannerId");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
