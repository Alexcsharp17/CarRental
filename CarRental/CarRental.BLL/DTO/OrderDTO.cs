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
{   [DateValid(ErrorMessage ="End Date Should be grater or equal to start.")]
   public class OrderDTO
    {
        [HiddenInput(DisplayValue = false)]
       
        public int OrderId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string User_Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CarId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start rent")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        
        [Display(Name = "End rent")]
        public DateTime EndTime { get; set; }
        //Client needs driver
        [Display(Name = "Driver necessity")]
        public bool Driver { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Status { get; set; }
       
        public int PassportNumb { get; set; }
        public string ManagComment { get; set; }
        public double OrdSum { get; set; }
        public CarDTO CarDTO { get; set; }
    }
}
