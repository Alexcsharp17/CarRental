using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Calculations
{
    /// <summary>
    ///Price calc is used to incapsulate all business-logic and calculations
    /// </summary>
   public class PriceCalc
    {
        public static int PricePerDays(int price, TimeSpan days)
        {
            if (days.Days < 3)
            {
                return Convert.ToInt32(Math.Round( price/24 *days.TotalHours));
            }
            else if (days.Days < 9)
            {
                return Convert.ToInt32(Math.Round( price * 0.9)/24*days.TotalHours);
            }
            else if (days.Days <= 25)
            {
                return Convert.ToInt32(Math.Round(price * 0.7)/24*days.TotalHours);
            }
            else if(days.Days > 26)
            {
                return Convert.ToInt32(Math.Round(price * 0.6)/24*days.TotalHours);
            }
            else
            {
                return Convert.ToInt32( price/24*days.TotalHours);
            }

        }
    }
}
