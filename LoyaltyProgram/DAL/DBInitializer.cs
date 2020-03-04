using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LoyaltyProgram.Models;
using System.Data.Entity.Migrations;

namespace LoyaltyProgram.DAL
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LoyaltyProgramContext>
    {
        protected override void Seed(LoyaltyProgramContext context)
        {
            context.Roles.AddOrUpdate(_ => _.RoleId, new Roles()
            {
                RoleId = 1,
                RoleName = "Super Admin",
                RoleDescription = "Super Admin Role"
            }
            , new Roles() {
                RoleId = 2,
                RoleName = "Admin",
                RoleDescription = "Admin Role"
            },
           new Roles()
           {
               RoleId = 2,
               RoleName = "Admin",
               RoleDescription = "Admin Role"
           });
          
        }
    }
}