namespace BookInventory.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookInventory.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookInventory.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Create test data for DiscountCode
            var discountCodes = new DiscountCode[]
            {
                new DiscountCode {DiscountCodeId = 1, Code = "SUMMERBR", Description = "Summer discount", DiscountValue = 0.1 },
                new DiscountCode {DiscountCodeId = 2, Code = "SPRINGBR", Description = "Spring discount", DiscountValue = 0.2 },
                new DiscountCode {DiscountCodeId = 3, Code = "SUPERSALE", Description = "Super sales discount", DiscountValue = 0.25 },
                new DiscountCode {DiscountCodeId = 4, Code = "OLDBOOKS", Description = "Old books discount", DiscountValue = 0.5 },
                new DiscountCode {DiscountCodeId = 5, Code = "BIGSALES", Description = "Big sales discount", DiscountValue = 0.75 }
            };

            context.DiscountCodes.AddOrUpdate(d => d.DiscountCodeId, discountCodes);

            // Create test data for LocationCode
            var locationCodes = new LocationCode[]
            {
                new LocationCode {Id = 1, Code = "3825BXBA", Description = "Cold storage under the basement" },
                new LocationCode {Id = 2, Code = "01EFF", Description = "Shelves 01 East first floor" },
                new LocationCode {Id = 3, Code = "02WFF", Description = "Shelves 02 West first floor" },
                new LocationCode {Id = 4, Code = "03SFF", Description = "Shelves 03 South first floor" },
                new LocationCode {Id = 5, Code = "04ESF", Description = "Shelves 04 East second floor" },
                new LocationCode {Id = 6, Code = "05WSF", Description = "Shelves 05 West second floor" },
                new LocationCode {Id = 7, Code = "06SSF", Description = "Shelves 06 South second floor" },
            };

            context.LocationCodes.AddOrUpdate(l => l.Id, locationCodes);
        }
    }
}
