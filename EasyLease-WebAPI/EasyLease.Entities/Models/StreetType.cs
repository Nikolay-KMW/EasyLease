using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class StreetType {
        [Column("StreetTypeId")]
        [MaxLength(50)]
        public string Id { get; set; }
    }
}