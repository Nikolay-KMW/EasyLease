using System;

namespace EasyLease.Entities.Models {
    public class AdvertComfort {
        public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }

        public string ComfortId { get; set; }
        public Comfort Comfort { get; set; }
    }
}