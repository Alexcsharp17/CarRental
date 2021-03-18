using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
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
        public int Id { get; set; }

        [Display(Name = "CarName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "NameRequired")]
        public string Name { get; set; }

        [Display(Name = "Manufacturer", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ManufacturerRequired")]
        public string Manufacturer { get; set; }

        [Display(Name = "Capacity", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "CapacityRequired")]
        [Range(1, 5, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "CapacityRange")]
        public int Capacity { get; set; }

        //(Four, front,rear)
        [Display(Name = "DriveUnit", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "DriveUnitRequired")]
        public string DriveUnit { get; set; }

        //(True-transsmision is automatic, False-manual)
        [Display(Name = "AutomaticTransmission", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TransmissionRequired")]
        public bool AutomaticTransm { get; set; }

        [Display(Name = "AirCondition", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "AirConditionRequired")]
        public bool AirConditon { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resource))]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PriceRange")]
        //The price is set in gryvnas per day.
        public int Price { get; set; }

        [Display(Name = "Door", ResourceType = typeof(Resource))]
        [Range(1, 10, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "DoorRange")]
        public int Door { get; set; }

        //Oil fuel/dizel fuel/
        [Display(Name = "FuelType", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FuelTypeRequired")]
        public string FuelType { get; set; }

        [Display(Name = "FuelConsumption", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FuelConsumptionRequired")]

        public int FuelConsump { get; set; }

        public bool IsDeleted { get; set; }

        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }

        [Display(Name = "CarType", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "CarTypeRequired")]
        public string CarType { get; set; }

        [Display(Name = "EngineCapacity", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EngineCapacityRequired")]

        public int EngSize { get; set; }

        public int Popular { get; set; }




    }
}
