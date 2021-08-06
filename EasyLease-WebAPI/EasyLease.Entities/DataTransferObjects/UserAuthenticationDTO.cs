using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLease.Entities.DataTransferObjects {
    public class UserAuthenticationDTO {
        [Required(ErrorMessage = "User name is a required field.")]
        [MinLength(2, ErrorMessage = "Minimum length for the name is 2 characters.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the name is 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(8, ErrorMessage = "Minimum length for the password is 8 characters.")]
        [MaxLength(1000, ErrorMessage = "Maximum length for the password is 1000 characters.")]
        public string Password { get; set; }
    }
}