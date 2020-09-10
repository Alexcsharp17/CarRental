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
    /// <summary>
    /// Application context used to connect to database  
    /// </summary>
    public class ApplicationContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }
        public ApplicationContext()
           : base("DefaultConnection")
        { }

        //Initializer call to fill db with default data
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new CarRentalDbInitializer()); //custom initializer
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<CarItem> CarItems { get; set; }
    

       
        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

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
