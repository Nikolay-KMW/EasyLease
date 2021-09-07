using System;
using System.Collections.Generic;

namespace EasyLease.Entities.DataTransferObjects {
    public class ProfileDTO {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedUser { get; set; }
        public DateTime VisitedUser { get; set; }
        public string Image { get; set; }
    }
}