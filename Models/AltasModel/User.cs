namespace Atlas.Models.AltasModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Attendees = new HashSet<Attendee>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int UserTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendee> Attendees { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
