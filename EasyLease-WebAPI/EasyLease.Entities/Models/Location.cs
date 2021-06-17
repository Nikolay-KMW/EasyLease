using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLease.Entities.Models {
    public class Location {
        [Required(ErrorMessage = "Region is a required field.")]
        [MaxLength(50)]
        public string Region { get; set; }

        [Required(ErrorMessage = "District is a required field.")]
        [MaxLength(50)]
        public string District { get; set; }

        public ICollection<Advert> Adverts { get; set; }
    }
}