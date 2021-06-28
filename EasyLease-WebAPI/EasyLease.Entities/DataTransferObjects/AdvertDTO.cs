using System;
using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    public class AdvertDTO {
        public Guid Id { get; set; }
        public string AdvertType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public int Area { get; set; }
        public int? Storey { get; set; }
        public int? NumberOfStoreys { get; set; }
        public string FullAddress { get; set; }
        public string[] Images { get; set; }
        public PriceType PriceType { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOfLease { get; set; }
        public DateTime? EndOfLease { get; set; }
        public DateTime CreatedAd { get; set; }
        public DateTime? UpdatedAd { get; set; }
        public string Slug { get; set; }
        public string[] ComfortList { get; set; }
        public string[] TagList { get; set; }
        public bool Favorited { get; set; }
        public ProfileDTO Author { get; set; }
    }
}