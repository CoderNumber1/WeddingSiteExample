using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class BlobContainerMap : EntityTypeConfiguration<BlobContainer>
    {
        public BlobContainerMap()
        {
            // Primary Key
            this.HasKey(t => t.BlobContainerId);

            // Properties
            this.Property(t => t.ContainerName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DisplayName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BlobContainer");
            this.Property(t => t.BlobContainerId).HasColumnName("BlobContainerId");
            this.Property(t => t.ContainerName).HasColumnName("ContainerName");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
        }
    }
}
