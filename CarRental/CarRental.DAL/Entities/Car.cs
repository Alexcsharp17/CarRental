using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Entities
{
   public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Capacity { get; set; }
        //(Four, front,rear)
        public string DriveUnit { get; set; }
        //(True-transsmision is automatic, False-manual)
        public byte AutomaticTransm { get; set; }
        public byte AirConditon{ get; set; }
       
        //The price is set in gryvnas per day.
        public decimal Price { get; set; }
        public virtual ICollection<CarType> CarTypes { get; set; }

        public Car()
        {
            CarTypes = new List<CarType>();
        }
    }
}
