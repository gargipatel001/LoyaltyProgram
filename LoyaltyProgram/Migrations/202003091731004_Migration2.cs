namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerLevel", "PointsRangeFrom", c => c.Double());
            AlterColumn("dbo.CustomerLevel", "PointsRangeTo", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerLevel", "PointsRangeTo", c => c.Double(nullable: false));
            AlterColumn("dbo.CustomerLevel", "PointsRangeFrom", c => c.Double(nullable: false));
        }
    }
}
