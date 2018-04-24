using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class UserClaimMap : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserClaim");
            this.Property(t => t.UserId).HasColumnName("UserID");
            this.Property(t => t.Id).HasColumnName("ClaimID");
            this.Property(t => t.ClaimType).HasColumnName("ClaimType");
            this.Property(t => t.ClaimValue).HasColumnName("ClaimValue");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Claims)
                .HasForeignKey(d => d.UserId);

        }
    }
}
