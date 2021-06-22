using System;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    public class AdvertDTO {
        public Guid Id { get; set; }
        public string AdvertTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullAddress { get; set; }
        public DateTime CreatedAd { get; set; }
        public DateTime? UpdatedAd { get; set; }
        public string Slug { get; set; }
        public PriceType PriceType { get; set; }
        public string[] TagList { get; set; }
        public bool Favorited { get; set; }
        //public ProfileDTO Author { get; set; }
    }
}