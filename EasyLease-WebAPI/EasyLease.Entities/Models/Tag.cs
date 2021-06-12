using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLease.Entities.Models {
    public class Tag {
        [Column("TagId")]
        [MaxLength(30, ErrorMessage = "Maximum length for the name of tag is 30 characters.")]
        public string Id { get; set; }

        public ICollection<AdvertTag> AdvertTags { get; set; }
    }

    // public class Tag {
    //     [Column("TagId")]
    //     public string Id { get; set; }

    //     [MaxLength(30, ErrorMessage = "Maximum length for the name of tag is 30 characters.")]
    //     public string TagName { get; set; }

    //     [ForeignKey(nameof(Advert))]
    //     public Guid AdvertId { get; set; }
    //     public Advert Advert { get; set; }
    // }
}