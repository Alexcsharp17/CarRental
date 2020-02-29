using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WEB.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderDTO> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}