
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Advert {
        [Column("AdvertId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Title is 150 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(1000, ErrorMessage = "Maximum length for the Description is 1000 characters.")]
        public string Description { get; set; }

        // =========== Full Address =============
        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Region is a required field.")]
        public Region Region { get; set; }

        [ForeignKey(nameof(District))]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "District is a required field.")]
        public District District { get; set; }

        [ForeignKey(nameof(SettlementType))]
        public int SettlementTypeId { get; set; }

        [Required(ErrorMessage = "Type of settlement is a required field.")]
        public SettlementType SettlementType { get; set; }

        [Required(ErrorMessage = "Name of settlement is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length of the settlement name is 100 characters.")]
        public string SettlementName { get; set; }

        [ForeignKey(nameof(StreetType))]
        public int StreetTypeId { get; set; }

        [Required(ErrorMessage = "Type of street is a required field.")]
        public Region StreetType { get; set; }

        [Required(ErrorMessage = "Name of street is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length of the street name is 150 characters.")]
        public string StreetName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length of house number is 50 characters.")]
        public string HouseNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        // ======================================

        public string Images { get; set; }
        public bool ActiveAd { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(PriceType))]
        public int PriceTypeId { get; set; }

        [Column(TypeName = "varchar(24)")]
        public PriceType PriceType { get; set; }
        public DateTime StartOfLease { get; set; }
        public DateTime? EndOfLease { get; set; }
        public DateTime CreatedAd { get; set; }
        public DateTime? UpdatedAd { get; set; }
        public string Slug { get; set; }
        public ICollection<AdvertTag> AdvertTags { get; set; }
        //public ICollection<Tag> Tags { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? FavoriteUserId { get; set; }
        public User Favorite { get; set; }

        [ForeignKey(nameof(User))]
        public Guid AuthorUserId { get; set; }
        public User Author { get; set; }
    }
}
