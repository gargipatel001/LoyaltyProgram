namespace LoyaltyProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsLoggedInFlagInCustomerTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "IsLoggedIn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "IsLoggedIn");
        }
    }
}
