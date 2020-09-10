using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.DTO
{
    public class CarItemDTO 
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string LicencePlate { get; set; }
    }
}
