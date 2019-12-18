namespace Atlas.Models.AltasModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resource")]
    public partial class Resource
    {
        public int ResourceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int StatusId { get; set; }

        public int ResourceTypeId { get; set; }

        public int? AttendeeId { get; set; }

        public int? MeetingId { get; set; }

        public virtual Meeting Meeting { get; set; }

        public virtual Attendee Attendee { get; set; }

        public virtual ResourceType ResourceType { get; set; }

        public virtual Status Status { get; set; }
    }
}
