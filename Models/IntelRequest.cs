namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IntelRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntelRequest()
        {
            IntelReqStatus = new HashSet<IntelReqStatu>();
        }

        [StringLength(36)]
        public string IntelRequestID { get; set; }

        public DateTime? OrignatedDateTime { get; set; }

        [StringLength(36)]
        public string OrginatorPersonnelID { get; set; }

        public int? AreaOfOperations { get; set; }

        [StringLength(50)]
        public string SupportedCommand { get; set; }

        public int? SupportedUnit { get; set; }

        [StringLength(50)]
        public string NamedOperation { get; set; }

        public int? MissionType { get; set; }

        public int? SubMissionType { get; set; }

        public DateTime? ActiveDateTimeStart { get; set; }

        public DateTime? ActiveDateTimeEnd { get; set; }

        public DateTime? BestCollectionTime { get; set; }

        public DateTime? LatestTimeIntelValue { get; set; }

        [StringLength(50)]
        public string PriorityIntelRequirement { get; set; }

        [StringLength(300)]
        public string SpecialInstructions { get; set; }

        [StringLength(36)]
        public string PrimaryPayload { get; set; }

        [StringLength(36)]
        public string SecondaryPayload { get; set; }

        [StringLength(36)]
        public string Armed { get; set; }

        [StringLength(36)]
        public string PointofContact { get; set; }

        [StringLength(3)]
        public string ReportClassification { get; set; }

        [StringLength(50)]
        public string LIMIDSRequest { get; set; }

        public virtual IC_ISM_Classifications IC_ISM_Classifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntelReqStatu> IntelReqStatus { get; set; }

        public virtual MissionType MissionType1 { get; set; }

        public virtual MissionType MissionType2 { get; set; }

        public virtual Payload Payload { get; set; }

        public virtual Payload Payload1 { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
