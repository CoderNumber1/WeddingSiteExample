using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class UserLoginMap : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey });

            // Properties
            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LoginProvider)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.ProviderKey)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("UserLogin");
            this.Property(t => t.UserId).HasColumnName("UserID");
            this.Property(t => t.LoginProvider).HasColumnName("LoginProvider");
            this.Property(t => t.ProviderKey).HasColumnName("ProviderKey");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Logins)
                .HasForeignKey(d => d.UserId);

        }
    }
}
