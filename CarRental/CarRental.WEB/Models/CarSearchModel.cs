using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Models
{
    public class CarSearchModel
    {

        public List<string> manufactorers=new List<string>();
     
        public List<string> CarTypes=new List<string>();
        public List<string> FuelTypes = new List<string>();
        public List<string> Transmissions = new List<string>();
        public List<string> Capacities = new List<string>();
        public List<string> FuelConsump = new List<string>();
        public List<string> EngSizes = new List<string>();
        public List<string> Doors = new List<string>();
        [Display(Name = "Model Name")]
        public  string Name { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int MinPrice { get; set; }
        [Display(Name = "From")]
        public int LowPrice { get; set; }
      public  int MaxPrice { get; set; }
        [Display(Name = "To")]
        public int UppPrice { get; set; }
         
    }
}