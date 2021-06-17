using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class SettlementType {
        [Column("SettlementTypeId")]
        [MaxLength(50)]
        public string Id { get; set; }
    }
}