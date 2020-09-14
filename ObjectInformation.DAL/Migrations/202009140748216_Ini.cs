namespace ObjectInformation.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checklist",
                c => new
                    {
                        ChecklistId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChecklistId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 100),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Region", t => t.RegionId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(nullable: false, maxLength: 100),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictId)
                .ForeignKey("dbo.City", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.ObjectRealty",
                c => new
                    {
                        ObjectRealtyId = c.Int(nullable: false, identity: true),
                        ObjectTypeId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        Name = c.String(maxLength: 200),
                        Cost = c.Double(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        CostDate = c.DateTime(),
                        DateOfSale = c.DateTime(),
                        CurrencyRate = c.Double(nullable: false),
                        Square = c.Double(nullable: false),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        lat = c.String(),
                        lng = c.String(),
                        zoom = c.String(),
                        CadastralNumber = c.String(),
                        CostDCT = c.Double(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ObjectRealtyId)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Currency", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.ObjectType", t => t.ObjectTypeId)
                .ForeignKey("dbo.Region", t => t.RegionId, cascadeDelete: true)
                .ForeignKey("dbo.District", t => t.DistrictId)
                .Index(t => t.ObjectTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.RegionId)
                .Index(t => t.CityId)
                .Index(t => t.DistrictId)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false, maxLength: 1000),
                        ObjectRealtyId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CommentDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.ObjectRealty", t => t.ObjectRealtyId, cascadeDelete: true)
                .Index(t => t.ObjectRealtyId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        RegionName = c.String(nullable: false, maxLength: 100),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegionId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.ObjectProperty",
                c => new
                    {
                        ObjectPropertyId = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 200),
                        ObjectRealtyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ObjectPropertyId)
                .ForeignKey("dbo.Property", t => t.PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.ObjectRealty", t => t.ObjectRealtyId, cascadeDelete: true)
                .Index(t => t.PropertyId)
                .Index(t => t.ObjectRealtyId);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Unit", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Category = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.ObjectType",
                c => new
                    {
                        ObjectTypeId = c.Int(nullable: false, identity: true),
                        ObjectTypeName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ObjectTypeId);
            
            CreateTable(
                "dbo.Upload",
                c => new
                    {
                        UploadId = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false, maxLength: 1000),
                        DocumentTypeId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 500),
                        FileHeader = c.String(),
                        FileDescription = c.String(),
                        ObjectRealtyId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UploadId)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ObjectRealty", t => t.ObjectRealtyId, cascadeDelete: true)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.ObjectRealtyId);
            
            CreateTable(
                "dbo.DocumentType",
                c => new
                    {
                        DocumentTypeId = c.Int(nullable: false, identity: true),
                        DocumentTypeName = c.String(nullable: false, maxLength: 200),
                        ChecklistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.Coordinate",
                c => new
                    {
                        CoordinateId = c.Int(nullable: false, identity: true),
                        PolygonId = c.Int(nullable: false),
                        lat = c.Double(nullable: false),
                        lng = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CoordinateId);
            
            CreateTable(
                "dbo.ObjectRealtyPledgers",
                c => new
                    {
                        ObjectRealtyPledgersId = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        ObjectRealtyId = c.Int(nullable: false),
                        PledgersId = c.Int(nullable: false),
                        PledgeDate = c.DateTime(),
                        MortgageValue = c.Double(nullable: false),
                        AssessedValue = c.Double(nullable: false),
                        EvaluationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ObjectRealtyPledgersId)
                .ForeignKey("dbo.Pledgers", t => t.PledgersId, cascadeDelete: true)
                .Index(t => t.PledgersId);
            
            CreateTable(
                "dbo.Pledgers",
                c => new
                    {
                        PledgersId = c.Int(nullable: false, identity: true),
                        NameOfPledger = c.String(),
                        ControllingShareholder = c.String(),
                    })
                .PrimaryKey(t => t.PledgersId);
            
            CreateTable(
                "dbo.Polygon",
                c => new
                    {
                        PolygonId = c.Int(nullable: false, identity: true),
                        PolygonName = c.String(),
                        PolygonDescription = c.String(),
                        ObjectRealtyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PolygonId);
            
            CreateTable(
                "dbo.PropertyObjectType",
                c => new
                    {
                        PropertyObjectTypeId = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        ObjectTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyObjectTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ObjectRealtyPledgers", "PledgersId", "dbo.Pledgers");
            DropForeignKey("dbo.District", "CityId", "dbo.City");
            DropForeignKey("dbo.ObjectRealty", "DistrictId", "dbo.District");
            DropForeignKey("dbo.Upload", "ObjectRealtyId", "dbo.ObjectRealty");
            DropForeignKey("dbo.Upload", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.ObjectRealty", "RegionId", "dbo.Region");
            DropForeignKey("dbo.ObjectRealty", "ObjectTypeId", "dbo.ObjectType");
            DropForeignKey("dbo.ObjectProperty", "ObjectRealtyId", "dbo.ObjectRealty");
            DropForeignKey("dbo.Property", "UnitId", "dbo.Unit");
            DropForeignKey("dbo.ObjectProperty", "PropertyId", "dbo.Property");
            DropForeignKey("dbo.ObjectRealty", "CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.ObjectRealty", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Region", "CountryId", "dbo.Country");
            DropForeignKey("dbo.City", "RegionId", "dbo.Region");
            DropForeignKey("dbo.Comment", "ObjectRealtyId", "dbo.ObjectRealty");
            DropForeignKey("dbo.ObjectRealty", "CityId", "dbo.City");
            DropIndex("dbo.ObjectRealtyPledgers", new[] { "PledgersId" });
            DropIndex("dbo.Upload", new[] { "ObjectRealtyId" });
            DropIndex("dbo.Upload", new[] { "DocumentTypeId" });
            DropIndex("dbo.Property", new[] { "UnitId" });
            DropIndex("dbo.ObjectProperty", new[] { "ObjectRealtyId" });
            DropIndex("dbo.ObjectProperty", new[] { "PropertyId" });
            DropIndex("dbo.Region", new[] { "CountryId" });
            DropIndex("dbo.Comment", new[] { "ObjectRealtyId" });
            DropIndex("dbo.ObjectRealty", new[] { "CurrencyId" });
            DropIndex("dbo.ObjectRealty", new[] { "DistrictId" });
            DropIndex("dbo.ObjectRealty", new[] { "CityId" });
            DropIndex("dbo.ObjectRealty", new[] { "RegionId" });
            DropIndex("dbo.ObjectRealty", new[] { "CountryId" });
            DropIndex("dbo.ObjectRealty", new[] { "ObjectTypeId" });
            DropIndex("dbo.District", new[] { "CityId" });
            DropIndex("dbo.City", new[] { "RegionId" });
            DropTable("dbo.PropertyObjectType");
            DropTable("dbo.Polygon");
            DropTable("dbo.Pledgers");
            DropTable("dbo.ObjectRealtyPledgers");
            DropTable("dbo.Coordinate");
            DropTable("dbo.DocumentType");
            DropTable("dbo.Upload");
            DropTable("dbo.ObjectType");
            DropTable("dbo.Unit");
            DropTable("dbo.Property");
            DropTable("dbo.ObjectProperty");
            DropTable("dbo.Currency");
            DropTable("dbo.Region");
            DropTable("dbo.Country");
            DropTable("dbo.Comment");
            DropTable("dbo.ObjectRealty");
            DropTable("dbo.District");
            DropTable("dbo.City");
            DropTable("dbo.Checklist");
        }
    }
}
