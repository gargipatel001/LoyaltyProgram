namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Tables1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "RoleName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "RoleName", c => c.String(maxLength: 10));
        }
    }
}
