using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WEB.Models
{
    public class CarSearchModel
    {
      public string Manufactorer { get; set; }
      public  string CarType { get; set; }
       public  string Name { get; set; }
      public int LowPrice { get; set; }
       public int UppPrice { get; set; }
         
    }
}