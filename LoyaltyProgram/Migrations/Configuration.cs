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

            //------ Sample data For Partner Table-------
            //  context.Partners.AddOrUpdate(_ => _.PartnerId, new Partner()
            //  {
            //      PartnerId = 1,
            //      PartnerName = "Levis",
            //      PartnerLogo = "Images/PromotionPartners/levis.jpg",
            //      PartnerDesc = "Levis Outfit",
            //      PartnerWebLink = "https://www.levi.com/CA/en_CA/"

            //  }
            //, new Partner()
            //{
            //    PartnerId = 2,
            //    PartnerName = "Cineplex",
            //    PartnerLogo = "Images/PromotionPartners/cineplex-logo.jpg",
            //    PartnerDesc = "Cineplex Movie",
            //    PartnerWebLink = "https://www.cineplex.com/"

            //}) ; 

            //------ Sample data For PromotionType Table-------
            //context.PromotionTypes.AddOrUpdate(_ => _.PromotionTypeId, new PromotionType()
            //{
            //    PromotionTypeId = 1,
            //    PromotionTypeName= "GV"

            //}); 

            //------ Sample data For Promotion Table-------
            //context.Promotions.AddOrUpdate(_ => _.PromotionId, new Promotion()
            //{
            //    PromotionId = 1,
            //    PromotionName = "Levis Rewards",
            //    PromotionTitle = "Levis Rewards",
            //    PromotionDesc = "Reedem points to buy Levis GV",
            //    PartnerId = 1,
            //    PromotionTypeId = 1,
            //    PromotionPoints = 1000
            //},
            //    new Promotion()
            //    {
            //        PromotionId = 2,
            //        PromotionName = "Cineplex",
            //        PromotionTitle = "Discount",
            //        PromotionDesc = "$5 discount on movie tickets",
            //        PartnerId = 2,
            //        PromotionTypeId = 1,
            //        PromotionPoints = 15000

            //    });

            //------ Sample data For Customer Table-------
            //context.Customers.AddOrUpdate(_ => _.CustomerId, new Customer()
            //{
            //    CustomerId = 1,
            //    CustomerEmail = "gargipatel001@gmail.com",
            //    CustomerPassword = "12345",
            //    CustomerFirstName = "Gargi",
            //    CustomerLastName = "Patel",
            //    CustomerAddress = "1 Lexus Drive",
            //    CustomerCity = "Etobicoke",
            //    CustomerProvince = "Ontario",
            //    CustomerPostalCode = "M9W6T3",
            //    CustomerDOB = DateTime.Now,
            //    CustomerPhoneNumber = "1234567890",
            //    CustomerGender = "F",
            //    CustomerCardNo = "12345AS69112" ,
            //    CreatedOn = DateTime.Now,
            //    CustomerLoyaltyPoints = 10000,
            //    RoleId = 3,
            //    LevelId = 2,
            //    IsLoggedIn = true
             
            //}) ;
        }
    }
}
