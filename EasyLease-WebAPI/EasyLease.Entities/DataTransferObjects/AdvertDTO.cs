using System;
using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    public class AdvertDTO {
        public Guid Id { get; set; }
        public string RealtyType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public int Area { get; set; }
        public int? Storey { get; set; }
        public int? NumberOfStoreys { get; set; }

        // =========== Full Address =============
        public string Region { get; set; }
        public string District { get; set; }
        public string SettlementType { get; set; }
        public string SettlementName { get; set; }
        public string StreetType { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        // ======================================

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