using System.Text.Json.Serialization;

namespace EasyLease.Entities.Models {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PriceType {
        PricePerDay,
        PricePerMonth
    }
}