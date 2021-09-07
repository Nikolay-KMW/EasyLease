using System;
using System.Collections.Generic;

namespace EasyLease.Entities.DataTransferObjects {
    public class UserDTO {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedUser { get; set; }
        public DateTime? UpdatedUser { get; set; }
        public DateTime VisitedUser { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}