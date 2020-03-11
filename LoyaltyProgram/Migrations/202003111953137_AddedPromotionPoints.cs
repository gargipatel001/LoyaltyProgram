namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPromotionPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotion", "PromotionPoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Promotion", "PromotionPoints");
        }
    }
}
