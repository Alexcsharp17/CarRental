using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WEB.Models
{
    public class OrderViewModel
    {
        public OrderDTO Order { get; set; }
        public CarDTO Car { get; set; }
    }
}