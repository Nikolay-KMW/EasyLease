using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Comfort {
        [Column("ComfortId")]
        [MaxLength(50)]
        public string Id { get; set; }

        public ICollection<AdvertComfort> AdvertComforts { get; set; }
    }
}