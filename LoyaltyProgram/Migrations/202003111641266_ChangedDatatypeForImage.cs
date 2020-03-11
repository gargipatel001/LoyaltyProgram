namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDatatypeForImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Partner", "PartnerLogo", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Partner", "PartnerLogo", c => c.String());
        }
    }
}
