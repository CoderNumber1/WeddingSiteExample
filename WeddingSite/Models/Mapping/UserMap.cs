using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.PasswordHash)
                .HasMaxLength(100);

            this.Property(t => t.SecurityStamp)
                .HasMaxLength(100);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.Id).HasColumnName("UserID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.EmailConfirmed).HasColumnName("EmailConfirmed");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.SecurityStamp).HasColumnName("SecurityStamp");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            this.Property(t => t.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
            this.Property(t => t.LockoutEndDateUtc).HasColumnName("LockoutEndDateUtc");
            this.Property(t => t.LockoutEnabled).HasColumnName("LockoutEnabled");
            this.Property(t => t.AccessFailedCount).HasColumnName("AccessFailedCount");
        }
    }
}
