using System;
using System.Collections.Generic;
using EasyLease.Entities.Extensions;
using EasyLease.Entities.Models;

namespace EasyLease.Entities.DataTransferObjects {
    public class AdvertAdditionalDataDTO {
        public string[] AdvertType { get; set; }
        public string[] SettlementType { get; set; }
        public string[] StreetType { get; set; }
        public AdvertLocationDTO[] Locations { get; set; }
        public string[] Comforts { get; set; }
        public PriceTypeDTO[] PriceType {
            get {
                List<PriceTypeDTO> priceTypeDTO = new List<PriceTypeDTO>();

                foreach (var name in Enum.GetNames(typeof(PriceType))) {
                    priceTypeDTO.Add(new PriceTypeDTO() { Name = name, Display = Enum.Parse<PriceType>(name).GetDisplayName() });
                }

                return priceTypeDTO.ToArray();
            }
        }
    }

    public class PriceTypeDTO {
        public string Name { get; set; }
        public string Display { get; set; }
    }
}