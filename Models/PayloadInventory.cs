﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    [Table("PayloadInventory")]
    public class PayloadInventory
    {
        public PayloadInventory()
        {
        }

        [Key]
        [StringLength(36)]
        public string id { get; set; }

        [Required]
        [StringLength(36)]
        public string metaDataID { get; set; }

        [Required]
        [StringLength(36)]
        public string locationID { get; set; }

        public int owningUnit { get; set; }

        [StringLength(50)]
        public string serialNumber { get; set; }

        [Required]
        public DateTime lastUpdate { get; set; }

        [Required]
        [StringLength(36)]
        public string lastUpdateUserId { get; set; }
    }
}