
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Advert {
        [Column("AdvertId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Title is 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(400, ErrorMessage = "Maximum length for the Description is 400 characters.")]
        public string Description { get; set; }
        public DateTime CreatedAd { get; set; }
        public DateTime? UpdatedAd { get; set; }
        public string Slug { get; set; }
        public ICollection<Tag> Tags { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User Author { get; set; }
    }
}
