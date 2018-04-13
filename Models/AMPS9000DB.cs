namespace AMPS9000_WebAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AMPS9000DB : DbContext
    {
        public AMPS9000DB()
            : base("name=AMPS9000DB")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<BranchOfService> BranchOfServices { get; set; }
        public virtual DbSet<COCOM> COCOMs { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DutyPosition> DutyPositions { get; set; }
        public virtual DbSet<EEIThreat> EEIThreats { get; set; }
        public virtual DbSet<GroundControlSystem> GroundControlSystems { get; set; }
        public virtual DbSet<IC_ISM_Classifications> IC_ISM_Classifications { get; set; }
        public virtual DbSet<IntelReqEEI> IntelReqEEIs { get; set; }
        public virtual DbSet<IntelReqStatu> IntelReqStatus { get; set; }
        public virtual DbSet<IntelRequest> IntelRequests { get; set; }
        public virtual DbSet<LIMIDSReq> LIMIDSReqs { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<MapLayerCategory> MapLayerCategories { get; set; }
        public virtual DbSet<MapLayer> MapLayers { get; set; }
        public virtual DbSet<MissionType> MissionTypes { get; set; }
        public virtual DbSet<MOS_Desc> MOS_Desc { get; set; }
        public virtual DbSet<MunitionRole> MunitionRoles { get; set; }
        public virtual DbSet<Munition> Munitions { get; set; }
        public virtual DbSet<MunitionStatu> MunitionStatus { get; set; }
        public virtual DbSet<OperationArea> OperationAreas { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<PayGrade> PayGrades { get; set; }
        public virtual DbSet<PayloadRole> PayloadRoles { get; set; }
        public virtual DbSet<Payload> Payloads { get; set; }
        public virtual DbSet<PEDPersonnel> PEDPersonnels { get; set; }
        public virtual DbSet<PEDTeam> PEDTeams { get; set; }
        public virtual DbSet<PEDType> PEDTypes { get; set; }
        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<PersonnelStatu> PersonnelStatus { get; set; }
        public virtual DbSet<PlatformCategory> PlatformCategories { get; set; }
        public virtual DbSet<PlatformRole> PlatformRoles { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<PlatformStatu> PlatformStatus { get; set; }
        public virtual DbSet<PointsofInterest> PointsofInterests { get; set; }
        public virtual DbSet<RankClassification> RankClassifications { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SpecialQualification> SpecialQualifications { get; set; }
        public virtual DbSet<StatusCode> StatusCodes { get; set; }
        public virtual DbSet<StatusCodeType> StatusCodeTypes { get; set; }
        public virtual DbSet<ThreatGroup> ThreatGroups { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<NatlImagery> NatlImageries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.AssetID)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.createUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<AssetType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<AssetType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Alerts)
                .WithOptional(e => e.AssetType1)
                .HasForeignKey(e => e.AssetType);

            modelBuilder.Entity<BranchOfService>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<BranchOfService>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<BranchOfService>()
                .HasMany(e => e.Personnels)
                .WithOptional(e => e.BranchOfService)
                .HasForeignKey(e => e.ServiceBranch);

            modelBuilder.Entity<COCOM>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<COCOM>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Personnels)
                .WithOptional(e => e.Company1)
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<Country>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<DutyPosition>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<DutyPosition>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<DutyPosition>()
                .HasMany(e => e.Personnels)
                .WithOptional(e => e.DutyPosition)
                .HasForeignKey(e => e.DutyPosition1);

            modelBuilder.Entity<DutyPosition>()
                .HasMany(e => e.Personnels1)
                .WithOptional(e => e.DutyPosition4)
                .HasForeignKey(e => e.DutyPosition2);

            modelBuilder.Entity<DutyPosition>()
                .HasMany(e => e.Personnels2)
                .WithOptional(e => e.DutyPosition5)
                .HasForeignKey(e => e.DutyPosition3);

            modelBuilder.Entity<EEIThreat>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<EEIThreat>()
                .HasMany(e => e.IntelReqEEIs)
                .WithOptional(e => e.EEIThreat)
                .HasForeignKey(e => e.threatGroupID);

            modelBuilder.Entity<GroundControlSystem>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<GroundControlSystem>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<IC_ISM_Classifications>()
                .Property(e => e.ClassificationMarkingValue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<IC_ISM_Classifications>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<IC_ISM_Classifications>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<IC_ISM_Classifications>()
                .HasMany(e => e.IntelRequests)
                .WithOptional(e => e.IC_ISM_Classifications)
                .HasForeignKey(e => e.ReportClassification);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.intelReqID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.targetName)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.targetNum)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.gridCoordinates)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.POI1_ID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.POI2_ID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.POI3_ID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqStatu>()
                .Property(e => e.IntelRequestID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.IntelRequestID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.OrginatorPersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.SupportedCommand)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.NamedOperation)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.PriorityIntelRequirement)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.SpecialInstructions)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.PrimaryPayload)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.SecondaryPayload)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.Armed)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.PointofContact)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.ReportClassification)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.LIMIDSRequest)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .HasMany(e => e.IntelReqStatus)
                .WithRequired(e => e.IntelRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LIMIDSReq>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<LIMIDSReq>()
                .HasMany(e => e.IntelReqEEIs)
                .WithOptional(e => e.LIMIDSReq)
                .HasForeignKey(e => e.LIMIDS_Req);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationDocument)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationMapImage)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationStreet)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationCity)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationPostalCode)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationCountry)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationLatitude)
                .HasPrecision(12, 9);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationLongitude)
                .HasPrecision(12, 9);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationMGRS)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationPointofContact)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationFrequency)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Location>()
                .Property(e => e.KML)
                .IsUnicode(false);

            modelBuilder.Entity<Manufacturer>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Manufacturer>()
                .Property(e => e.languageCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MapLayerCategory>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayerCategory>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayerCategory>()
                .Property(e => e.createUserId)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayer>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayer>()
                .Property(e => e.fileLocation)
                .IsUnicode(false);

            modelBuilder.Entity<MissionType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MissionType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<MissionType>()
                .HasMany(e => e.IntelRequests)
                .WithOptional(e => e.MissionType1)
                .HasForeignKey(e => e.MissionType);

            modelBuilder.Entity<MissionType>()
                .HasMany(e => e.IntelRequests1)
                .WithOptional(e => e.MissionType2)
                .HasForeignKey(e => e.SubMissionType);

            modelBuilder.Entity<MOS_Desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MOS_Desc>()
                .Property(e => e.MOSCode)
                .IsUnicode(false);

            modelBuilder.Entity<MOS_Desc>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Munitions)
                .WithOptional(e => e.MOS_Desc)
                .HasForeignKey(e => e.MunitionMOS1);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Munitions1)
                .WithOptional(e => e.MOS_Desc1)
                .HasForeignKey(e => e.MunitionMOS2);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Munitions2)
                .WithOptional(e => e.MOS_Desc2)
                .HasForeignKey(e => e.MunitionMOS3);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Payloads)
                .WithOptional(e => e.MOS_Desc)
                .HasForeignKey(e => e.PayloadMOS1);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Payloads1)
                .WithOptional(e => e.MOS_Desc1)
                .HasForeignKey(e => e.PayloadMOS2);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Payloads2)
                .WithOptional(e => e.MOS_Desc2)
                .HasForeignKey(e => e.PayloadMOS3);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Personnels)
                .WithOptional(e => e.MOS_Desc)
                .HasForeignKey(e => e.MOS1);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Personnels1)
                .WithOptional(e => e.MOS_Desc1)
                .HasForeignKey(e => e.MOS2);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Personnels2)
                .WithOptional(e => e.MOS_Desc2)
                .HasForeignKey(e => e.MOS3);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Platforms)
                .WithOptional(e => e.MOS_Desc)
                .HasForeignKey(e => e.PlatformFlightCrewMOS);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Platforms1)
                .WithOptional(e => e.MOS_Desc1)
                .HasForeignKey(e => e.PlatformLineCrewMOS);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Platforms2)
                .WithOptional(e => e.MOS_Desc2)
                .HasForeignKey(e => e.PlatformPayloadCrewMOS);

            modelBuilder.Entity<MOS_Desc>()
                .HasMany(e => e.Platforms3)
                .WithOptional(e => e.MOS_Desc3)
                .HasForeignKey(e => e.PlatformPEDCrewMOS);

            modelBuilder.Entity<MunitionRole>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionRole>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionRole>()
                .HasMany(e => e.Munitions)
                .WithOptional(e => e.MunitionRole1)
                .HasForeignKey(e => e.MunitionRole);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionID)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionsReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWireframe)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.Munition3D)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.Munition2525B)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionDatasheet)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionName)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionNomenclature)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionManufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionExecutiveAgent)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionContractProgram)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionCost)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionCostNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionLength)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWidthDiameter)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWingspan)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWarhead)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionEngine)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionGuideanceSys)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionLaunchPlatform)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWeatherRestriction)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionStatu>()
                .Property(e => e.MunitionID)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionStatu>()
                .Property(e => e.StatusComments)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionStatu>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<OperationArea>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<OperationArea>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<OperationArea>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubCMD1)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubCMD2)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderDetail)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderLegacyDoc)
                .IsUnicode(false);

            modelBuilder.Entity<OrderType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<OrderType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<OrderType>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.OrderType1)
                .HasForeignKey(e => e.OrderType);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.GradeType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.PayGrade1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.DisplayText)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<PayGrade>()
                .HasMany(e => e.Personnels)
                .WithOptional(e => e.PayGrade1)
                .HasForeignKey(e => e.PayGrade);

            modelBuilder.Entity<PayloadRole>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadRole>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadRole>()
                .Property(e => e.roleCode)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadID)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PaylodWireframe)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.Payload3D)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.Payload2525B)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadDatasheet)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadName)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadNomenclature)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadManufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadExecutiveAgent)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadContractProgram)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCost)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCostNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadLength)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadWidth)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadHeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadPower)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadConnector1)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadConnector2)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCrewCount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .HasMany(e => e.IntelRequests)
                .WithOptional(e => e.Payload)
                .HasForeignKey(e => e.PrimaryPayload);

            modelBuilder.Entity<Payload>()
                .HasMany(e => e.IntelRequests1)
                .WithOptional(e => e.Payload1)
                .HasForeignKey(e => e.SecondaryPayload);

            modelBuilder.Entity<Payload>()
                .HasMany(e => e.Platforms)
                .WithOptional(e => e.Payload)
                .HasForeignKey(e => e.PlatformPayload1);

            modelBuilder.Entity<Payload>()
                .HasMany(e => e.Platforms1)
                .WithOptional(e => e.Payload1)
                .HasForeignKey(e => e.PlatformPayload2);

            modelBuilder.Entity<Payload>()
                .HasMany(e => e.Platforms2)
                .WithOptional(e => e.Payload2)
                .HasForeignKey(e => e.PlatformPayload3);

            modelBuilder.Entity<PEDPersonnel>()
                .Property(e => e.PEDID)
                .IsUnicode(false);

            modelBuilder.Entity<PEDPersonnel>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.PEDID)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.Identifier)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.StatusComments)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .HasMany(e => e.PEDPersonnels)
                .WithRequired(e => e.PEDTeam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PEDType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<PEDType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<PEDType>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<PEDType>()
                .HasMany(e => e.PEDTeams)
                .WithRequired(e => e.PEDType)
                .HasForeignKey(e => e.TypeCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.PersonnelReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.PersonnelPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.MiddleInitial)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Nationality)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Clearance)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.CACid)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.CallSign)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.DSN)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.EmailNIPR)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.EmailSIPR)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.ChatID)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelStatu>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelStatu>()
                .Property(e => e.PersonnelRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelStatu>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformCategory>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<PlatformCategory>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformCategory>()
                .HasMany(e => e.Platforms)
                .WithOptional(e => e.PlatformCategory1)
                .HasForeignKey(e => e.PlatformCategory);

            modelBuilder.Entity<PlatformRole>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformRole>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformRole>()
                .HasMany(e => e.Platforms)
                .WithOptional(e => e.PlatformRole1)
                .HasForeignKey(e => e.PlatformRole);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformID)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformWireframe)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.Platform3D)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.Platform2525B)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformDatasheet)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformTailNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformName)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformNomenclature)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformManufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformExecutiveAgent)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformContractProgram)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformCost)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformLength)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformWingspan)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformHeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPowerPlant)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPayloadCapacity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPayload1)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPayload2)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPayload3)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformArmamentCapacity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformArmament1)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformArmament2)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformArmament3)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformComs1)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformComs2)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformStatu>()
                .Property(e => e.PlatformID)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformStatu>()
                .Property(e => e.StatusComments)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformStatu>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.PointofInterestID)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.ReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.Document)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.MGRS)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.Elevation)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.createUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PointsofInterest>()
                .Property(e => e.KML)
                .IsUnicode(false);

            modelBuilder.Entity<RankClassification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<RankClassification>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .Property(e => e.description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .Property(e => e.rankAbbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .HasMany(e => e.Personnels)
                .WithOptional(e => e.Rank1)
                .HasForeignKey(e => e.Rank);

            modelBuilder.Entity<Region>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Region>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<SpecialQualification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<SpecialQualification>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<StatusCode>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<StatusCode>()
                .Property(e => e.displayOrder)
                .IsFixedLength();

            modelBuilder.Entity<StatusCode>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<StatusCode>()
                .HasMany(e => e.IntelReqStatus)
                .WithOptional(e => e.StatusCode)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<StatusCode>()
                .HasMany(e => e.MunitionStatus)
                .WithRequired(e => e.StatusCode1)
                .HasForeignKey(e => e.StatusCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StatusCodeType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<StatusCodeType>()
                .HasMany(e => e.StatusCodes)
                .WithRequired(e => e.StatusCodeType)
                .HasForeignKey(e => e.type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThreatGroup>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ThreatGroup>()
                .Property(e => e.groupCode)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.IntelRequests)
                .WithOptional(e => e.Unit)
                .HasForeignKey(e => e.SupportedUnit);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Personnels)
                .WithOptional(e => e.Unit)
                .HasForeignKey(e => e.DeployedUnit);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Personnels1)
                .WithOptional(e => e.Unit1)
                .HasForeignKey(e => e.AssignedUnit);

            modelBuilder.Entity<User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Alerts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.createUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Alerts1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.lastUpdateUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.MunitionStatus)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.lastUpdateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PEDTeams)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.lastUpdateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PersonnelStatus)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.lastUpdateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.NatImageryID)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.ReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.MGRS)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.NatlImgaryImage)
                .IsUnicode(false);
        }
    }
}
