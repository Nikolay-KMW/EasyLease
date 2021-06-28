
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Advert {
        [Column("AdvertId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(AdvertType))]
        public string AdvertTypeId { get; set; }

        [Required(ErrorMessage = "Type of advert is a required field.")]
        public AdvertType AdvertType { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Title is 150 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(1000, ErrorMessage = "Maximum length for the Description is 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Number of rooms is a required field.")]
        public int NumberOfRooms { get; set; }

        [Required(ErrorMessage = "Area is a required field.")]
        public int Area { get; set; }
        public int? Storey { get; set; }
        public int? NumberOfStoreys { get; set; }

        // =========== Full Address =============
        public string Region { get; set; }
        public string District { get; set; }

        [Required(ErrorMessage = "Location is a required field.")]
        public Location Location { get; set; }

        [ForeignKey(nameof(SettlementType))]
        public string SettlementTypeId { get; set; }

        [Required(ErrorMessage = "Type of settlement is a required field.")]
        public SettlementType SettlementType { get; set; }

        [Required(ErrorMessage = "Name of settlement is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length of the settlement name is 100 characters.")]
        public string SettlementName { get; set; }

        [ForeignKey(nameof(StreetType))]
        public string StreetTypeId { get; set; }

        [Required(ErrorMessage = "Type of street is a required field.")]
        public StreetType StreetType { get; set; }

        [Required(ErrorMessage = "Name of street is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length of the street name is 150 characters.")]
        public string StreetName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length of house number is 50 characters.")]
        public string HouseNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        // ======================================

        public ICollection<Image> Images { get; set; }

        [Required()]
        [Column(TypeName = "varchar(24)")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Type of price is a required field.")]
        [Column(TypeName = "varchar(24)")]
        public PriceType PriceType { get; set; }
        public decimal Price { get; set; }

        public DateTime StartOfLease { get; set; }
        public DateTime? EndOfLease { get; set; }
        public DateTime CreatedAd { get; set; }
        public DateTime? UpdatedAd { get; set; }
        //public string Slug { get; set; }

        public ICollection<AdvertComfort> AdvertComforts { get; set; }
        public ICollection<AdvertTag> AdvertTags { get; set; }

        // [ForeignKey(nameof(User))]
        // public Guid? FavoriteUserId { get; set; }
        // public User Favorite { get; set; }
        public ICollection<AdvertFavorite> AdvertFavorites { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User Author { get; set; }
    }
}
