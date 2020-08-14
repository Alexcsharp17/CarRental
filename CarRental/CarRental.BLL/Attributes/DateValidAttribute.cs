using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Attributes
{/// <summary>
/// Used to validate car rent time span 
/// </summary>
   public class DateValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            OrderDTO o = value as OrderDTO;
            if (o.StartTime >= o.EndTime )
            {
                return false;
            }
            return true;
        }
    }
}
