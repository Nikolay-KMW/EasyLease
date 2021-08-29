using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyLease.Entities.Models {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PriceType {
        [Display(Name = "за день")]
        PricePerDay,
        [Display(Name = "за месяц")]
        PricePerMonth
    }
}