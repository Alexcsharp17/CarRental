using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Entities
{   /// <summary>
    /// This class is used to describe order entity
    /// </summary>
    public class Order : BaseEntity
    {

        [Required]
        public string User_Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string DriverLicenceNumber { get; set; }

        [Required]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string StartPlace { get; set; }

        [Required]
        [MaxLength(100)]
        public string EndPlace { get; set; }

        //Client needs driver
        public bool Driver { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(300)]
        public string ManagComment { get; set; }

        [Required]
        public double OrdSum { get; set; }
    }
}
