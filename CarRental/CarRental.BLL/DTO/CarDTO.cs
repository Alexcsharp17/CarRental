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
    /// CarDTO - special Data transfer object.
    /// Used to transfer data of car entity between DAL and BLL(Layers)
    /// </summary>
   public class CarDTO
    {
        [HiddenInput(DisplayValue = false)]
        public int CarId { get; set; }

        [Display(Name = "Car Name")]
        [Required(ErrorMessage = "Please input car name")]
        public string Name { get; set; }

        [Display(Name = "Manufactorer")]
        [Required(ErrorMessage = "Please input manufactorer")]
        public string Manufacturer { get; set; }

        [Display(Name = "Capacity")]
        [Required(ErrorMessage = "Please input capacity")]
        [Range(1, 5, ErrorMessage = "The value must be in range from 1 to 5")]
        public int Capacity { get; set; }

        //(Four, front,rear)
        [Display(Name = "Drive Unit")]
        [Required(ErrorMessage = "Please input DriveUnit type")]
        public string DriveUnit { get; set; }

        //(True-transsmision is automatic, False-manual)
        [Display(Name = "Automatic transmission")]
        [Required(ErrorMessage = "Please select transmission type")]
        public bool AutomaticTransm { get; set; }

        [Display(Name = "Air Condition")]
        [Required(ErrorMessage = "Please select air condition.")]
        public bool AirConditon { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please input price")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be more than 0")]
        //The price is set in gryvnas per day.
        public int Price { get; set; }

        [Display(Name = "Door")]
        [Range(1, 10, ErrorMessage = "Input value between 1 and 10")]
        public int Door { get; set; }

        //Oil fuel/dizel fuel/
        [Display(Name = "FuelType")]
        public string FuelType { get; set; }

        [Display(Name = "Fuel consumption")]
     
        public int FuelConsump { get; set; }

        public bool IsDeleted { get; set; }

        public string Image { get; set; }

        [Display(Name = "Car type")]
        public string CarType { get; set; }

        [Display(Name = "Engine capacity")]
        
        public int EngSize { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        
        public CarDTO()
        {
            
            Orders = new List<Order>();
        }
    }
}
