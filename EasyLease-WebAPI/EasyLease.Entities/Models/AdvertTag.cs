using System;

namespace EasyLease.Entities.Models {
    public class AdvertTag {
        public DateTime CreatedTag { get; set; }

        public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}