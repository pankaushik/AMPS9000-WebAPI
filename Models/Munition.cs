namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Munition
    {
        [StringLength(36)]
        public string MunitionID { get; set; }

        [StringLength(50)]
        public string MunitionsReferenceCode { get; set; }

        [StringLength(150)]
        public string MunitionWireframe { get; set; }

        [StringLength(150)]
        public string MunitionPhoto { get; set; }

        [StringLength(150)]
        public string Munition3D { get; set; }

        [StringLength(150)]
        public string MunitionIcon { get; set; }

        [StringLength(150)]
        public string Munition2525B { get; set; }

        [StringLength(150)]
        public string MunitionDatasheet { get; set; }

        [StringLength(50)]
        public string MunitionName { get; set; }

        [StringLength(50)]
        public string MunitionNomenclature { get; set; }

        public int? MunitionRole { get; set; }

        [StringLength(50)]
        public string MunitionManufacturer { get; set; }

        [StringLength(50)]
        public string MunitionExecutiveAgent { get; set; }

        [StringLength(50)]
        public string MunitionContractProgram { get; set; }

        public decimal? MunitionCost { get; set; }

        [StringLength(300)]
        public string MunitionCostNotes { get; set; }

        public decimal? MunitionLength { get; set; }

        public decimal? MunitionWidthDiameter { get; set; }

        public decimal? MunitionWeight { get; set; }

        public decimal? MunitionWingspan { get; set; }

        [StringLength(50)]
        public string MunitionWarhead { get; set; }

        [StringLength(50)]
        public string MunitionEngine { get; set; }

        public int? MunitionRange { get; set; }

        public int? MunitionSpeed { get; set; }

        [StringLength(50)]
        public string MunitionGuideanceSys { get; set; }

        [StringLength(50)]
        public string MunitionLaunchPlatform { get; set; }

        [StringLength(50)]
        public string MunitionWeatherRestriction { get; set; }

        public int? MunitionCrewCount { get; set; }

        public int? MunitionMOS1 { get; set; }

        public int? MunitionMOS2 { get; set; }

        public int? MunitionMOS3 { get; set; }

        public virtual MOS_Desc MOS_Desc { get; set; }

        public virtual MOS_Desc MOS_Desc1 { get; set; }

        public virtual MOS_Desc MOS_Desc2 { get; set; }

        public virtual MunitionRole MunitionRole1 { get; set; }
    }
}
