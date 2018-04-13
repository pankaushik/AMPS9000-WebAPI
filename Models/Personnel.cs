namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personnel")]
    public partial class Personnel
    {
        [StringLength(36)]
        public string PersonnelID { get; set; }

        [StringLength(50)]
        public string PersonnelReferenceCode { get; set; }

        [StringLength(150)]
        public string PersonnelPhoto { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleInitial { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public int? PayGrade { get; set; }

        public int? Rank { get; set; }

        [StringLength(2)]
        public string Nationality { get; set; }

        [StringLength(2)]
        public string Clearance { get; set; }

        [StringLength(20)]
        public string CACid { get; set; }

        [StringLength(50)]
        public string CallSign { get; set; }

        public int? ServiceBranch { get; set; }

        public int? Company { get; set; }

        public int? AssignedUnit { get; set; }

        public int? DeployedUnit { get; set; }

        public int? MOS1 { get; set; }

        public int? MOS2 { get; set; }

        public int? MOS3 { get; set; }

        public int? DutyPosition1 { get; set; }

        public int? DutyPosition2 { get; set; }

        public int? DutyPosition3 { get; set; }

        public int? SpecialQuals1 { get; set; }

        public int? SpecialQuals2 { get; set; }

        public int? SpecialQuals3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CurrentAssignmentStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CurrentAssignmentEnd { get; set; }

        [StringLength(50)]
        public string DSN { get; set; }

        [StringLength(50)]
        public string EmailNIPR { get; set; }

        [StringLength(50)]
        public string EmailSIPR { get; set; }

        [StringLength(50)]
        public string ChatID { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdate { get; set; }

        public DateTime? LastLogin { get; set; }

        public virtual BranchOfService BranchOfService { get; set; }

        public virtual Company Company1 { get; set; }

        public virtual DutyPosition DutyPosition { get; set; }

        public virtual DutyPosition DutyPosition4 { get; set; }

        public virtual DutyPosition DutyPosition5 { get; set; }

        public virtual MOS_Desc MOS_Desc { get; set; }

        public virtual MOS_Desc MOS_Desc1 { get; set; }

        public virtual MOS_Desc MOS_Desc2 { get; set; }

        public virtual PayGrade PayGrade1 { get; set; }

        public virtual Rank Rank1 { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual Unit Unit1 { get; set; }
    }
}
