using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
   public class Car : BaseEntity
    {
        
        
        [Required(ErrorMessage = "Please input car name")]
        [MaxLength(100)]
        public string Name { get; set; }

       
        [Required(ErrorMessage = "Please input manufactorer")]
        [MaxLength(100)]
        public string Manufacturer { get; set; }

        [Display(Name = "Capacity")]
        [Required(ErrorMessage = "Please input capacity")]
        [Range(1, 5, ErrorMessage = "The value must be in range from 1 to 5")]
        public byte Capacity { get; set; }

        //(Four, front,rear)
        [Column(TypeName = "varchar")] //Ascki symbols only(max lenght 10)
        [MaxLength(10)]
        [Required(ErrorMessage = "Please input DriveUnit type")]
        public string DriveUnit { get; set; }

        //(True-transsmision is automatic, False-manual)
       
        [Required(ErrorMessage = "Please select transmission type")]
        
        public bool AutomaticTransm { get; set; }

        
        [Required(ErrorMessage = "Please select air condition.")]
        public bool AirConditon { get; set; }

        
        [Required(ErrorMessage = "Please input price")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be more than 0")]        
        //The price is set in gryvnas per day.
        public int Price { get; set; }

        
        [Range(1, 10, ErrorMessage = "Input value between 1 and 10")]
        public byte Door { get; set; }

        //Oil fuel/dizel fuel/
        [Display(Name = "FuelType")]
        [MaxLength(15)]
        public string FuelType { get; set; }

        public byte FuelConsump { get; set; }

       
        public byte EngSize { get; set; }

        public string Image { get; set; }

        [MaxLength(50)]
        [Required]
        public string CarType { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
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
