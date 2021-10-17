using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    public abstract class AdvertManipulationDTO : IValidatableObject {
        [Required(ErrorMessage = "Type of realty is a required field.")]
        public string RealtyType { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Title is 150 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(10000, ErrorMessage = "Maximum length for the Description is 10000 characters.")]
        public string Description { get; set; }

        [Range(1, 500, ErrorMessage = "Number of rooms is required and it can't be less then 1 and more then 500")]
        public int NumberOfRooms { get; set; }

        [Range(1, 10000, ErrorMessage = "Area is required and it can't be less than 1 m2 and more then 10000 m2.")]
        public int Area { get; set; }

        [Range(1, 1000, ErrorMessage = "Storey can't be be below than 1 and above then 1000.")]
        public int? Storey { get; set; }

        [Range(1, 1000, ErrorMessage = "Number of storey can't be less then 1 and more then 1000.")]
        public int? NumberOfStoreys { get; set; }

        // =========== Full Address =============
        [Required(ErrorMessage = "Region is a required field.")]
        public string Region { get; set; }

        [Required(ErrorMessage = "District is a required field.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Type of settlement is a required field.")]
        public string SettlementType { get; set; }

        [Required(ErrorMessage = "Name of settlement is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length of the settlement name is 100 characters.")]
        public string SettlementName { get; set; }

        [Required(ErrorMessage = "Type of street is a required field.")]
        public string StreetType { get; set; }

        [Required(ErrorMessage = "Name of street is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length of the street name is 150 characters.")]
        public string StreetName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length of house number is 50 characters.")]
        public string HouseNumber { get; set; }

        [Range(1, 10000, ErrorMessage = "Apartament number can't be less then 1 and more then 10000.")]
        public int? ApartmentNumber { get; set; }
        // ======================================

        // [EnumDataType(typeof(PriceType))]
        // public PriceType PriceType { get; set; }
        public string PriceType { get; set; }

        [Required(ErrorMessage = "Price is a required field.")]
        [Range(0, 1000000, ErrorMessage = "Price can't be more then 1000000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Start of lease is a required field.")]
        public DateTime StartOfLease { get; set; }
        public DateTime? EndOfLease { get; set; }

        public string[] ComfortList { get; set; }
        public string[] TagList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (TagList.Length > 5) {
                yield return new ValidationResult("The list of tags can't be more then 5 words.", new[] { nameof(TagList) });
            }

            foreach (string tag in TagList) {
                if (tag.Length < 1 || tag.Length > 30) {
                    yield return new ValidationResult("Tag can't be lower than 1 and bigger then 30 characters.",
                        new[] { nameof(TagList) });
                }
            }

            ICollection<string> tagsWithoutDuplicates = new List<string>();

            foreach (var tag in TagList) {
                if (!tagsWithoutDuplicates.Contains(tag)) {
                    tagsWithoutDuplicates.Add(tag);
                }
            }

            TagList = tagsWithoutDuplicates.ToArray();
        }
    }
}