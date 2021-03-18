using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.DTO
{
    public class CarTypeDTO
    {
        public int CarTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CarDTO> Cars { get; set; }

        public CarTypeDTO()
        {
            Cars = new List<CarDTO>();
        }
    }
}
