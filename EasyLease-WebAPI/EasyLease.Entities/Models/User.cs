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
        public string UserName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [EmailAddress(ErrorMessage = "Email Address is not valid.")]
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public DateTime CreatedUser { get; set; }
        public DateTime? UpdatedUser { get; set; }
        //public ICollection<Advert> Favorites { get; set; }
        public ICollection<Advert> Adverts { get; set; }
    }
}