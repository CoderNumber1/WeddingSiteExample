using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WeddingSite.Models.Mapping
{
    public class GuestMap : EntityTypeConfiguration<Guest>
    {
        public GuestMap()
        {
            // Primary Key
            this.HasKey(t => t.GuestId);

            // Properties
            this.Property(t => t.GuestCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.AddressOne)
                .HasMaxLength(500);

            this.Property(t => t.AddressTwo)
                .HasMaxLength(500);

            this.Property(t => t.City)
                .HasMaxLength(250);

            this.Property(t => t.State)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Country)
                .HasMaxLength(250);

            this.Property(t => t.DriveFromAddressOne)
                .HasMaxLength(500);

            this.Property(t => t.DriveFromAddressTwo)
                .HasMaxLength(500);

            this.Property(t => t.DriveFromCity)
                .HasMaxLength(250);

            this.Property(t => t.DriveFromState)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.DriveFromCountry)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Guest");
            this.Property(t => t.GuestId).HasColumnName("GuestId");
            this.Property(t => t.GuestCode).HasColumnName("GuestCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.RSVPFlag).HasColumnName("RSVPFlag");
            this.Property(t => t.MaxAllowed).HasColumnName("MaxAllowed");
            this.Property(t => t.NumberAttending).HasColumnName("NumberAttending");
            this.Property(t => t.Invited).HasColumnName("Invited");
            this.Property(t => t.AddressOne).HasColumnName("AddressOne");
            this.Property(t => t.AddressTwo).HasColumnName("AddressTwo");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.DriveFromMailTo).HasColumnName("DriveFromMailTo");
            this.Property(t => t.DriveFromAddressOne).HasColumnName("DriveFromAddressOne");
            this.Property(t => t.DriveFromAddressTwo).HasColumnName("DriveFromAddressTwo");
            this.Property(t => t.DriveFromCity).HasColumnName("DriveFromCity");
            this.Property(t => t.DriveFromState).HasColumnName("DriveFromState");
            this.Property(t => t.DriveFromCountry).HasColumnName("DriveFromCountry");
            this.Property(t => t.SaveDateSent).HasColumnName("SaveDateSent");
            this.Property(t => t.InvitationSent).HasColumnName("InvitationSent");
        }
    }
}
