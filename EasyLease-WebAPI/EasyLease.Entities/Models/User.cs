using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class User {
        [Column("UserId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User name is a required field.")]
        [MinLength(2, ErrorMessage = "Maximum length for the name is 2 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the name is 20 characters.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the second name is 50 characters.")]
        public string SecondName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the third name is 50 characters.")]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [EmailAddress(ErrorMessage = "Email Address is not valid.")]
        public string Email { get; set; }
        public string Biography { get; set; }

        [MaxLength(126976, ErrorMessage = "Maximum size for the Avatar photo is 62KB.")]
        public byte[] Avatar { get; set; }
        public DateTime CreatedUser { get; set; }
        public DateTime? UpdatedUser { get; set; }

        public ICollection<AdvertFavorite> AdvertFavorites { get; set; }
        public ICollection<Advert> Adverts { get; set; }
    }
}