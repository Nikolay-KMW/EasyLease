using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Image {
        [Column("ImageId")]
        public Guid Id { get; set; }

        [Required()]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required()]
        [MaxLength(200)]
        public string Path { get; set; }

        [ForeignKey(nameof(Advert))]
        public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}