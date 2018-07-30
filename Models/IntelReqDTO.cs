using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public class IntelReqDTO
    {
        public string IntelRequestID { get; set; }

        public int ReqUserFrndlyID { get; set; }

        public DateTime? OrignatedDateTime { get; set; }
     
        public string OrginatorPersonnelID { get; set; }

        public int? AreaOfOperations { get; set; }

        public int SupportedCommand { get; set; }
        public string COCOMText { get; set; }

        public int? SupportedUnit { get; set; }
        

        public string NamedOperation { get; set; }

        public int? MissionType { get; set; }
        public string MissionTypeText { get; set; }

        public int? SubMissionType { get; set; }

        public DateTime? ActiveDateTimeStart { get; set; }

        public DateTime? ActiveDateTimeEnd { get; set; }

        public DateTime? BestCollectionTime { get; set; }

        public DateTime? LatestTimeIntelValue { get; set; }

        public string PriorityIntelRequirement { get; set; }

        public string SpecialInstructions { get; set; }

        public string PrimaryPayload { get; set; }
        public string PrimaryPayloadName { get; set; }

        public string SecondaryPayload { get; set; }
        public string SecondaryPayloadName { get; set; }

        public string Armed { get; set; }
        public string MunitionName { get; set; }

        public string PointofContact { get; set; }
        public string POCText { get; set; }

        public string ReportClassification { get; set; }
        public string RptClassText { get; set; }

        public string LIMIDSRequest { get; set; }
        

    }
}