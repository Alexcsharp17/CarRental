using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WEB.Areas.Admin.Models
{
    public class StatisticsModel
    {
        public Dictionary<string, int> DaySales { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> CarSales { get; set; } = new Dictionary<string, int>();

        public List<int> MonthList { get; set; }

        public int? SelectedMonth { get; set; }

        public bool DataExist { get; set; }
    }
}