using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLease.Entities.DataTransferObjects {
    public class ProfileUpdateDTO {
        [Required(ErrorMessage = "User name is a required field.")]
        [MinLength(2, ErrorMessage = "Minimum length for the name is 2 characters.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the name is 50 characters.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the second name is 50 characters.")]
        public string SecondName { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the third name is 50 characters.")]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Email Address is not valid.")]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the Biography is 500 characters.")]
        public string Bio { get; set; }
    }
}