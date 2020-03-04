namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_User_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        UserPassword = c.String(),
                        UserFirstName = c.String(),
                        UserLastName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "RoleID", "dbo.Roles");
            DropIndex("dbo.User", new[] { "RoleID" });
            DropTable("dbo.User");
        }
    }
}
