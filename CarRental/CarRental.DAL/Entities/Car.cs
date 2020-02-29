using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarRental.DAL.Entities
{
    /// <summary>
    /// This clas is used to describe entity of car,
    /// which is stored in database and to transfer data about the 
    /// car to upper architecture level
    /// </summary>
   public class Car
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
        [Display(Name = "DriveUnit")]
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
        public decimal Price { get; set; }

        [Display(Name = "Door")]
        [Range(1, 10, ErrorMessage = "Input value between 1 and 10")]
        public int Door { get; set; }

        //Oil fuel/dizel fuel/
        [Display(Name = "FuelType")]
        public string FuelType { get; set; }

        [Display(Name = "Fuel consumption")]
        public int FuelConsump { get; set; }
        public int EngSize { get; set; }

        public bool IsDeleted { get; set; }

        public string Image { get; set; }

        [Display(Name = "Car type")]
        public string CarType { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        /// <summary>
        /// Constructor is used to create initialize CarTypes list and Orders list,
        /// Which will be later used for creating relation between models
        /// </summary>
        public Car()
        {
            
            Orders = new List<Order>();
        }
     

    }
}
