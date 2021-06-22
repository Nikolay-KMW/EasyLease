using System;
using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    public class AdvertCreationDTO {
        public string AdvertTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int NumberOfRooms { get; set; }
        public int Area { get; set; }
        public int Storey { get; set; }
        public int NumberOfStoreys { get; set; }

        // =========== Full Address =============
        public string Region { get; set; }
        public string District { get; set; }
        public string SettlementTypeId { get; set; }
        public string SettlementName { get; set; }
        public string StreetTypeId { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        // ======================================

        public string Images { get; set; }
        public PriceType PriceType { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOfLease { get; set; }
        public DateTime? EndOfLease { get; set; }
        //public string Slug { get; set; }

        //public ICollection<AdvertComfort> AdvertComforts { get; set; }
        //public ICollection<AdvertTag> AdvertTags { get; set; }
    }
}