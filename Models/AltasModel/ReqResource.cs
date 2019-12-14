namespace Atlas.Models.AltasModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReqResource")]
    public partial class ReqResource
    {
        public int ReqResourceId { get; set; }

        public int MeetingId { get; set; }

        public int ResourceTypeId { get; set; }

        public virtual Meeting Meeting { get; set; }

        public virtual ResourceType ResourceType { get; set; }
    }
}
