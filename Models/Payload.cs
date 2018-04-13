namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payload
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payload()
        {
            IntelRequests = new HashSet<IntelRequest>();
            IntelRequests1 = new HashSet<IntelRequest>();
            Platforms = new HashSet<Platform>();
            Platforms1 = new HashSet<Platform>();
            Platforms2 = new HashSet<Platform>();
        }

        [StringLength(36)]
        public string PayloadID { get; set; }

        [StringLength(50)]
        public string PayloadReferenceCode { get; set; }

        [StringLength(150)]
        public string PaylodWireframe { get; set; }

        [StringLength(150)]
        public string PayloadPhoto { get; set; }

        [StringLength(150)]
        public string Payload3D { get; set; }

        [StringLength(150)]
        public string PayloadIcon { get; set; }

        [StringLength(150)]
        public string Payload2525B { get; set; }

        [StringLength(150)]
        public string PayloadDatasheet { get; set; }

        [StringLength(50)]
        public string PayloadName { get; set; }

        [StringLength(50)]
        public string PayloadNomenclature { get; set; }

        public int? PayloadRole { get; set; }

        [StringLength(50)]
        public string PayloadManufacturer { get; set; }

        [StringLength(50)]
        public string PayloadExecutiveAgent { get; set; }

        [StringLength(50)]
        public string PayloadContractProgram { get; set; }

        public decimal? PayloadCost { get; set; }

        [StringLength(300)]
        public string PayloadCostNotes { get; set; }

        public decimal? PayloadLength { get; set; }

        public decimal PayloadWidth { get; set; }

        public decimal? PayloadHeight { get; set; }

        public decimal? PayloadWeight { get; set; }

        public decimal? PayloadPower { get; set; }

        [StringLength(50)]
        public string PayloadConnector1 { get; set; }

        [StringLength(50)]
        public string PayloadConnector2 { get; set; }

        public bool? PayloadDaySpotter { get; set; }

        public bool? PayloadThermalImager { get; set; }

        public bool? PayloadLaserDesginator { get; set; }

        public bool? PayloadContinuousZoom { get; set; }

        public bool? PayloadStabalization { get; set; }

        public bool? PayloadVibrationIsolation { get; set; }

        public bool? PayloadAutoTracker { get; set; }

        public bool? PayloadGPSTimeSync { get; set; }

        public bool? PayloadInternalGPS { get; set; }

        public bool? PayloadInternalINS { get; set; }

        public bool? PayloadMetadata { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PayloadCrewCount { get; set; }

        public int? PayloadMOS1 { get; set; }

        public int? PayloadMOS2 { get; set; }

        public int? PayloadMOS3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntelRequest> IntelRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntelRequest> IntelRequests1 { get; set; }

        public virtual MOS_Desc MOS_Desc { get; set; }

        public virtual MOS_Desc MOS_Desc1 { get; set; }

        public virtual MOS_Desc MOS_Desc2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Platform> Platforms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Platform> Platforms1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Platform> Platforms2 { get; set; }
    }
}
