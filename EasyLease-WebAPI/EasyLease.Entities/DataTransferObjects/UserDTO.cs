using System;
using System.Collections.Generic;

namespace EasyLease.Entities.DataTransferObjects {
    public class UserDTO {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public DateTime CreatedUser { get; set; }
        public DateTime? UpdatedUser { get; set; }
        public string Token { get; set; }
    }
}