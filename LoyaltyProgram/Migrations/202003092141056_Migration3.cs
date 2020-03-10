namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customer", "Level_LevelId", "dbo.CustomerLevel");
            DropIndex("dbo.Customer", new[] { "Level_LevelId" });
            RenameColumn(table: "dbo.Customer", name: "Level_LevelId", newName: "LevelId");
            AlterColumn("dbo.Customer", "LevelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customer", "LevelId");
            AddForeignKey("dbo.Customer", "LevelId", "dbo.CustomerLevel", "LevelId", cascadeDelete: true);
            DropColumn("dbo.Customer", "CustomerLevelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "CustomerLevelId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Customer", "LevelId", "dbo.CustomerLevel");
            DropIndex("dbo.Customer", new[] { "LevelId" });
            AlterColumn("dbo.Customer", "LevelId", c => c.Int());
            RenameColumn(table: "dbo.Customer", name: "LevelId", newName: "Level_LevelId");
            CreateIndex("dbo.Customer", "Level_LevelId");
            AddForeignKey("dbo.Customer", "Level_LevelId", "dbo.CustomerLevel", "LevelId");
        }
    }
}
