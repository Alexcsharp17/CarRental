using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Entities
{   /// <summary>
/// This class is used to describe order entity
/// </summary>
   public class Order
    {
        public int OrderId { get; set; }

        public string User_Id { get; set; }
        public ApplicationUser User { get; set; }
        public int PassportNumb { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    
        public string StartPlace { get; set; }
      
        public string EndPlace { get; set; }
        
        //Client needs driver
        public bool Driver { get; set; }
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        public string ManagComment { get; set; }
        public double OrdSum { get; set; }
    }
}
