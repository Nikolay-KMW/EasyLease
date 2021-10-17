using System;
using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    public class AdvertsDTO {
        public Guid Id { get; set; }
        public string RealtyType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullAddress { get; set; }
        public ImageDTO Image { get; set; }
        public DateTime CreatedAd { get; set; }
        public string Slug { get; set; }
        public PriceType PriceType { get; set; }
        public decimal Price { get; set; }
        public string[] ComfortList { get; set; }
        public string[] TagList { get; set; }
        public bool Favorited { get; set; }
    }
}