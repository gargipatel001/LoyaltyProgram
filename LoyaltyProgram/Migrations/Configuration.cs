namespace LoyaltyProgram.Migrations
{
    using LoyaltyProgram.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LoyaltyProgram.DAL.LoyaltyProgramContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LoyaltyProgram.DAL.LoyaltyProgramContext context)
        {
            context.Roles.AddOrUpdate(_ => _.RoleId, new Roles()
            {
                RoleId = 1,
                RoleName = "Super Admin",
                RoleDescription = "Super Admin Role"
            }
           , new Roles()
           {
               RoleId = 2,
               RoleName = "Admin",
               RoleDescription = "Admin Role"
           },
          new Roles()
          {
              RoleId = 3,
              RoleName = "User",
              RoleDescription = "User Role"
          }); 
        }
    }
}
