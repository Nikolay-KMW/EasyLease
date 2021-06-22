using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class AdvertType {
        [Column("AdvertTypeId")]
        [MaxLength(50)]
        public string Id { get; set; }
    }
}