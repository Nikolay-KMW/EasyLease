using System;
using System.Diagnostics.CodeAnalysis;

namespace EasyLease.Entities.Models {
    public class FavoriteAdvert {
        public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}