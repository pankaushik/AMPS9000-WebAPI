namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unit()
        {
           
        }

        public int id { get; set; }

        [Required]
        public int branchSubordinateTier1ID { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

        [StringLength(100)]
        public string HQCity { get; set; }

        [StringLength(2)]
        public string HQStateAbbrev { get; set; }

        [StringLength(10)]
        public string HQCountryCode { get; set; }
    }
}
