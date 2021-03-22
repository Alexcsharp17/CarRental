using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WEB.Models
{
    public class PagingInfo
    {
        // Items quantity
        public int TotalItems { get; set; }

        // emount of items per page
        public int ItemsPerPage { get; set; }

        // Current page number
        public int CurrentPage { get; set; }

        // Pages quantity
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}