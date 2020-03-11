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
         //   context.Roles.AddOrUpdate(_ => _.RoleId, new Roles()
         //   {
         //       RoleId = 1,
         //       RoleName = "Super Admin",
         //       RoleDescription = "Super Admin Role"
         //   }
         //  , new Roles()
         //  {
         //      RoleId = 2,
         //      RoleName = "Admin",
         //      RoleDescription = "Admin Role"
         //  },
         // new Roles()
         // {
         //     RoleId = 3,
         //     RoleName = "Customer",
         //     RoleDescription = "Customer Role"
         // });
         //   context.CustomerLevels.AddOrUpdate(_ => _.LevelId, new CustomerLevel()
         //   {
         //       LevelId = 1,
         //       LevelName = "Silver",
         //       PointsRangeFrom = 0,
         //       PointsRangeTo = 1000

         //   },
         //  new CustomerLevel()
         //  {
         //      LevelId = 2,
         //      LevelName = "Gold",
         //      PointsRangeFrom = 1001,
         //      PointsRangeTo = 10000

         //  },
         //new CustomerLevel()
         //{
         //    LevelId = 3,
         //    LevelName = "Diamond",
         //    PointsRangeFrom = 10001,
         //    PointsRangeTo = 25000

         //},
         //new CustomerLevel()
         //{
         //    LevelId = 4,
         //    LevelName = "Platinum",
         //    PointsRangeFrom = 25001


         //});
        }
    }
}
