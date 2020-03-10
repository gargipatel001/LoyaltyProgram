namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerLevel",
                c => new
                    {
                        LevelId = c.Int(nullable: false, identity: true),
                        LevelName = c.String(maxLength: 25),
                        PointsRangeFrom = c.Double(nullable: false),
                        PointsRangeTo = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LevelId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerEmail = c.String(),
                        CustomerPassword = c.String(),
                        CustomerFirstName = c.String(maxLength: 25),
                        CustomerLastName = c.String(maxLength: 25),
                        CustomerAddress = c.String(),
                        CustomerCity = c.String(maxLength: 25),
                        CustomerProvince = c.String(maxLength: 25),
                        CustomerPostalCode = c.String(maxLength: 6),
                        CustomerDOB = c.DateTime(),
                        CustomerPhoneNumber = c.String(maxLength: 10),
                        CustomerGender = c.String(maxLength: 1),
                        CustomerCardNo = c.String(maxLength: 16),
                        CreatedOn = c.DateTime(),
                        CustomerLoyaltyPoints = c.Double(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CustomerLevelId = c.Int(nullable: false),
                        Level_LevelId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.CustomerLevel", t => t.Level_LevelId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.Level_LevelId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleDescription = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        UserPassword = c.String(),
                        UserFirstName = c.String(maxLength: 25),
                        UserLastName = c.String(maxLength: 25),
                        CreatedOn = c.DateTime(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.FAQ",
                c => new
                    {
                        FAQId = c.Int(nullable: false, identity: true),
                        Question = c.String(maxLength: 10),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.FAQId);
            
            CreateTable(
                "dbo.Partner",
                c => new
                    {
                        PartnerId = c.Int(nullable: false, identity: true),
                        PartnerName = c.String(maxLength: 25),
                        PartnerLogo = c.String(),
                        PartnerDesc = c.String(),
                        PartnerWebLink = c.String(),
                    })
                .PrimaryKey(t => t.PartnerId);
            
            CreateTable(
                "dbo.PointRedeemHistory",
                c => new
                    {
                        RedeemHistoryId = c.Int(nullable: false, identity: true),
                        PointsRedeemed = c.Double(nullable: false),
                        PointsRedeemedOn = c.DateTime(),
                        Notes = c.String(),
                        PromotionId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RedeemHistoryId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Promotion", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.PromotionId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Promotion",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        PromotionName = c.String(maxLength: 25),
                        PromotionTitle = c.String(maxLength: 25),
                        PromotionDesc = c.String(),
                        PromotionStartDate = c.DateTime(),
                        PromotionEndDate = c.DateTime(),
                        PartnerId = c.Int(nullable: false),
                        PromotionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionId)
                .ForeignKey("dbo.Partner", t => t.PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.PromotionType", t => t.PromotionTypeId, cascadeDelete: true)
                .Index(t => t.PartnerId)
                .Index(t => t.PromotionTypeId);
            
            CreateTable(
                "dbo.PromotionType",
                c => new
                    {
                        PromotionTypeId = c.Int(nullable: false, identity: true),
                        PromotionTypeName = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.PromotionTypeId);
            
            CreateTable(
                "dbo.RewardPointHistory",
                c => new
                    {
                        PointHistoryId = c.Int(nullable: false, identity: true),
                        PointsEarned = c.Double(nullable: false),
                        PointsEarnedSource = c.String(),
                        PointsEarnedOn = c.DateTime(),
                        PointsExpireOn = c.DateTime(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PointHistoryId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RewardPointHistory", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.PointRedeemHistory", "PromotionId", "dbo.Promotion");
            DropForeignKey("dbo.Promotion", "PromotionTypeId", "dbo.PromotionType");
            DropForeignKey("dbo.Promotion", "PartnerId", "dbo.Partner");
            DropForeignKey("dbo.PointRedeemHistory", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.User", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Customer", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Customer", "Level_LevelId", "dbo.CustomerLevel");
            DropIndex("dbo.RewardPointHistory", new[] { "CustomerId" });
            DropIndex("dbo.Promotion", new[] { "PromotionTypeId" });
            DropIndex("dbo.Promotion", new[] { "PartnerId" });
            DropIndex("dbo.PointRedeemHistory", new[] { "CustomerId" });
            DropIndex("dbo.PointRedeemHistory", new[] { "PromotionId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.Customer", new[] { "Level_LevelId" });
            DropIndex("dbo.Customer", new[] { "RoleId" });
            DropTable("dbo.RewardPointHistory");
            DropTable("dbo.PromotionType");
            DropTable("dbo.Promotion");
            DropTable("dbo.PointRedeemHistory");
            DropTable("dbo.Partner");
            DropTable("dbo.FAQ");
            DropTable("dbo.User");
            DropTable("dbo.Roles");
            DropTable("dbo.Customer");
            DropTable("dbo.CustomerLevel");
        }
    }
}
