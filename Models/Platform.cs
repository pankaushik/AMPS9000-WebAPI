namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Platform
    {
        [StringLength(36)]
        public string PlatformID { get; set; }

        [StringLength(150)]
        public string PlatformWireframe { get; set; }

        [StringLength(150)]
        public string PlatformPhoto { get; set; }

        [StringLength(150)]
        public string Platform3D { get; set; }

        [StringLength(150)]
        public string PlatformIcon { get; set; }

        [StringLength(150)]
        public string Platform2525B { get; set; }

        [StringLength(150)]
        public string PlatformDatasheet { get; set; }

        [StringLength(50)]
        public string PlatformTailNumber { get; set; }

        [StringLength(50)]
        public string PlatformName { get; set; }

        [StringLength(50)]
        public string PlatformNomenclature { get; set; }

        public int? PlatformCategory { get; set; }

        public int? PlatformService { get; set; }

        public int? PlatformRole { get; set; }

        [StringLength(50)]
        public string PlatformManufacturer { get; set; }

        [StringLength(50)]
        public string PlatformExecutiveAgent { get; set; }

        [StringLength(50)]
        public string PlatformContractProgram { get; set; }

        public decimal? PlatformCost { get; set; }

        [StringLength(300)]
        public string PlatformCostNotes { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PlatformIOCDate { get; set; }

        public int? PlatformGroundStation { get; set; }

        public decimal? PlatformLength { get; set; }

        public decimal? PlatformWingspan { get; set; }

        public decimal? PlatformHeight { get; set; }

        public decimal? PlatformWeight { get; set; }

        [StringLength(50)]
        public string PlatformPowerPlant { get; set; }

        public int? PlatformFuelCapacity { get; set; }

        public int? PlatformCruisingSpeed { get; set; }

        public int? PlatformRange { get; set; }

        public int? PlatformCeiling { get; set; }

        public int? PlatformMaxTakeOff { get; set; }

        public int? PlatformMinRunway { get; set; }

        public decimal? PlatformPayloadCapacity { get; set; }

        public int? PlatformPayloadCount { get; set; }

        [StringLength(36)]
        public string PlatformPayload1 { get; set; }

        [StringLength(36)]
        public string PlatformPayload2 { get; set; }

        [StringLength(36)]
        public string PlatformPayload3 { get; set; }

        public decimal? PlatformArmamentCapacity { get; set; }

        public int? PlatformArmamentCount { get; set; }

        [StringLength(36)]
        public string PlatformArmament1 { get; set; }

        [StringLength(36)]
        public string PlatformArmament2 { get; set; }

        [StringLength(36)]
        public string PlatformArmament3 { get; set; }

        [StringLength(36)]
        public string PlatformComs1 { get; set; }

        [StringLength(36)]
        public string PlatformComs2 { get; set; }

        public int? PlatformFlightCrewReq { get; set; }

        public int? PlatformLineCrewReq { get; set; }

        public int? PlatformPayloadCrewReq { get; set; }

        public int? PlatformPEDCrewReq { get; set; }

        public int? PlatformFlightCrewMOS { get; set; }

        public int? PlatformLineCrewMOS { get; set; }

        public int? PlatformPayloadCrewMOS { get; set; }

        public int? PlatformPEDCrewMOS { get; set; }

        public virtual MOS_Desc MOS_Desc { get; set; }

        public virtual MOS_Desc MOS_Desc1 { get; set; }

        public virtual MOS_Desc MOS_Desc2 { get; set; }

        public virtual MOS_Desc MOS_Desc3 { get; set; }

        public virtual Payload Payload { get; set; }

        public virtual Payload Payload1 { get; set; }

        public virtual Payload Payload2 { get; set; }

        public virtual PlatformCategory PlatformCategory1 { get; set; }

        public virtual PlatformRole PlatformRole1 { get; set; }
    }
}
