using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using CarRental.DAL.Entities;

namespace CarRental.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Car> Cars { get; set; }
    
        public DbSet<Order> Orders { get; set; }
      // static ApplicationContext()
      // {
      //     Database.SetInitializer<ApplicationContext>(new CarRentalDbInitializer());
      // }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Car>().HasMany(c => c.CarTypes)
        //        .WithMany(t => t.Cars)
        //        .Map(t => t.MapLeftKey("CarId")
        //        .MapRightKey("CarTypeId")
        //        .ToTable("CarCarType"));
        //}

    }
}
