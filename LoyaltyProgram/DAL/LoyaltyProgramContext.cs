using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LoyaltyProgram.Models;


namespace LoyaltyProgram.DAL
{
    public class LoyaltyProgramContext : DbContext
    {
        public LoyaltyProgramContext() : base("name=LoyaltyProgramDBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LoyaltyProgramContext, LoyaltyProgram.Migrations.Configuration>());

        }
        public DbSet<Roles> Roles{get;set;}
        public DbSet<CustomerLevel> CustomerLevels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PointRedeemHistory> PointRedeemHistories { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionType> PromotionTypes { get; set; }
        public DbSet<RewardPointHistory> RewardPointHistories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}