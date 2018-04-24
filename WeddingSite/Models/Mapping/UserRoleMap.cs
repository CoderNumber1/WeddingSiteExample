using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserRole");
            this.Property(t => t.Id).HasColumnName("RoleID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
