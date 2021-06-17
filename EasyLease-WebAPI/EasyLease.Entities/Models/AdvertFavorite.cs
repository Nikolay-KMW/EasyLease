using System;

namespace EasyLease.Entities.Models {
    public class AdvertFavorite {
        public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}