using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
namespace CarRental.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        
        public bool Banned { get; set; }
        public string Name { get; set; }
        public int PassportNumb { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string RepairInvoice { get; set; }
        public ApplicationUser()
        {
            Orders = new List<Order>();
        }
    }
}
