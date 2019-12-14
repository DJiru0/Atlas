namespace Atlas.Models.AltasModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendee")]
    public partial class Attendee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attendee()
        {
            Resources = new HashSet<Resource>();
        }

        public int AttendeeId { get; set; }

        public int MeetingId { get; set; }

        public int UserId { get; set; }

        public virtual Meeting Meeting { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
