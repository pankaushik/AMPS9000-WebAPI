namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MunitionStatu
    {
        [Key]
        [StringLength(36)]
        public string MunitionID { get; set; }

        public int StatusCode { get; set; }

        [StringLength(300)]
        public string StatusComments { get; set; }

        public DateTime ETIC { get; set; }

        public DateTime lastUpdate { get; set; }

        [Required]
        [StringLength(36)]
        public string lastUpdateUserId { get; set; }

        public virtual StatusCode StatusCode1 { get; set; }

        public virtual User User { get; set; }
    }
}
