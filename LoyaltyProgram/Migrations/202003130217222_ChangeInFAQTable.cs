namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInFAQTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FAQ", "Question", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FAQ", "Question", c => c.String(maxLength: 10));
        }
    }
}
