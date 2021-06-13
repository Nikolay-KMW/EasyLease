using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Street {
        [Column("StreetTypeId")]
        public int Id { get; set; }

        [Required()]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}