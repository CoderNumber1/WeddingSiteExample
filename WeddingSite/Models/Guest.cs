using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class Guest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Guest()
        {
            GuestCodes = new HashSet<GuestCode>();
        }

        public int GuestId { get; set; }
        public string GuestCode { get; set; }
        public string Name { get; set; }
        public bool RSVPFlag { get; set; }
        public int MaxAllowed { get; set; }
        public int NumberAttending { get; set; }
        public bool Invited { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool DriveFromMailTo { get; set; }
        public string DriveFromAddressOne { get; set; }
        public string DriveFromAddressTwo { get; set; }
        public string DriveFromCity { get; set; }
        public string DriveFromState { get; set; }
        public string DriveFromCountry { get; set; }
        public bool SaveDateSent { get; set; }
        public bool InvitationSent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuestCode> GuestCodes { get; set; }
    }
}
