using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class UserUserRoleMap : EntityTypeConfiguration<UserUserRole>
    {
        public UserUserRoleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserId, t.RoleId });

            // Properties
            //this.Property(t => t.ID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UserUserRole");
            //this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserId).HasColumnName("UserID");
            this.Property(t => t.RoleId).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Roles)
                .HasForeignKey(d => d.UserId);
            this.HasRequired(t => t.UserRole)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
