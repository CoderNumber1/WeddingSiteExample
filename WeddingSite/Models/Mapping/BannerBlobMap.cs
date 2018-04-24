using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class BannerBlobMap : EntityTypeConfiguration<BannerBlob>
    {
        public BannerBlobMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerBlobId);

            // Properties
            // Table & Column Mappings
            this.ToTable("BannerBlob");
            this.Property(t => t.BannerBlobId).HasColumnName("BannerBlobId");
            this.Property(t => t.BannerId).HasColumnName("BannerId");
            this.Property(t => t.BlobId).HasColumnName("BlobId");

            // Relationships
            this.HasRequired(t => t.Banner)
                .WithMany(t => t.BannerBlobs)
                .HasForeignKey(d => d.BannerId);
            this.HasRequired(t => t.Blob)
                .WithMany(t => t.BannerBlobs)
                .HasForeignKey(d => d.BlobId);

        }
    }
}
