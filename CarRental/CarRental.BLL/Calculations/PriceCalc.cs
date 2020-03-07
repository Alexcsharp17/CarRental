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
        public static int PricePerDays(int price, int days)
        {
            if (days < 3)
            {
                return price;
            }
            else if (days < 9)
            {
                return Convert.ToInt32(Math.Round( price * 0.9));
            }
            else if (days <= 25)
            {
                return Convert.ToInt32(Math.Round(price * 0.7));
            }
            else if(days > 26)
            {
                return Convert.ToInt32(Math.Round(price * 0.6));
            }
            else
            {
                return price;
            }

        }
    }
}
