namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        [StringLength(36)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string LocationReferenceCode { get; set; }

        [StringLength(150)]
        public string LocationPhoto { get; set; }

        [StringLength(150)]
        public string LocationDocument { get; set; }

        [StringLength(150)]
        public string LocationMapImage { get; set; }

        [StringLength(50)]
        public string LocationName { get; set; }

        [StringLength(50)]
        public string LocationStreet { get; set; }

        [StringLength(50)]
        public string LocationCity { get; set; }

        [StringLength(10)]
        public string LocationPostalCode { get; set; }

        [StringLength(2)]
        public string LocationCountry { get; set; }

        public int? LocationCOCOM { get; set; }

        public int? LocationRegion { get; set; }

        public decimal? LocationLatitude { get; set; }

        public decimal? LocationLongitude { get; set; }

        [StringLength(50)]
        public string LocationMGRS { get; set; }

        public int? LocationElevation { get; set; }

        [StringLength(36)]
        public string LocationPointofContact { get; set; }

        public decimal? LocationFrequency { get; set; }

        [StringLength(150)]
        public string KML { get; set; }
    }
}
