using System;
using System.Collections.Generic;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    // Intermediate Data Transfer Object 
    public class AdvertAdditionalDataIDTO {
        public IEnumerable<RealtyType> RealtyType { get; set; }
        public IEnumerable<SettlementType> SettlementType { get; set; }
        public IEnumerable<StreetType> StreetType { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Comfort> Comforts { get; set; }
    }
}