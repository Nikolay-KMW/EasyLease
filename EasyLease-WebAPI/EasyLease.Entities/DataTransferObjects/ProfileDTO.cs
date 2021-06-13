using System;

namespace EasyLease.Entities.DataTransferObjects {
    public class ProfileDTO {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
    }
}