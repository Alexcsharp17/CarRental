using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
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
            if (o.StartTime != new DateTime(1, 1, 1) && o.EndTime != new DateTime(1, 1, 1))
            {
                return true;
            }
            return false;
        }
    }
}
