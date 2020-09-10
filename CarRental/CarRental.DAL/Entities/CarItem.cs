using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Entities
{
    public class CarItem : BaseEntity
    {
        public int CarId { get; set; }
        public string LicencePlate { get; set; }
    }
}
