using CarRental.BLL.Attributes;
using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarRental.BLL.DTO
{
    /// <summary>
    /// OrderDTO - special Data transfer object.
    /// Used to transfer data of car entity between DAL and BLL(Layers)
    /// </summary>

    [DateValid(ErrorMessage ="End Date Should be grater or equal to start.")]
   public class OrderDTO
    {
        [HiddenInput(DisplayValue = false)]
       
        public int Id { get; set; }
      
        public string User_Id { get; set; }
        
        public int CarId { get; set; }
        
        [Display(Name = "Start rent")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Take car")]
        public string StartPlace { get; set; }
        [Display(Name = "Return car")]
        public string EndPlace { get; set; }
        [Display(Name = "End rent")]
        public DateTime EndTime { get; set; }
        //Client needs driver
        [Display(Name = "Driver necessity")]
        public bool Driver { get; set; }
       
        public bool IsDeleted { get; set; }
        
        public string Status { get; set; }
       
        public int PassportNumb { get; set; }
        public string ManagComment { get; set; }
        public double OrdSum { get; set; }
        public CarDTO Car { get; set; }
    }
}
