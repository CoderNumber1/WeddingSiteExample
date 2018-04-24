using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class BlobMap : EntityTypeConfiguration<Blob>
    {
        public BlobMap()
        {
            // Primary Key
            this.HasKey(t => t.BlobId);

            // Properties
            this.Property(t => t.AltTag)
                .HasMaxLength(100);

            this.Property(t => t.DisplayName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.URL)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Blob");
            this.Property(t => t.BlobId).HasColumnName("BlobId");
            this.Property(t => t.BlobContainerId).HasColumnName("BlobContainerId");
            this.Property(t => t.AltTag).HasColumnName("AltTag");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.URL).HasColumnName("URL");

            // Relationships
            this.HasOptional(t => t.BlobContainer)
                .WithMany(t => t.Blobs)
                .HasForeignKey(d => d.BlobContainerId);

        }
    }
}
