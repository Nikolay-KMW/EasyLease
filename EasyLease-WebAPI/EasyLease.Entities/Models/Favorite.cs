using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Favorite {
        [Column("FavoriteId")]
        public Guid Id { get; set; }
        public ICollection<Advert> Adverts { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}