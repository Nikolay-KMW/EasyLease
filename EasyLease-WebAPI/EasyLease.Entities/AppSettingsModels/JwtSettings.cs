namespace EasyLease.Entities.AppSettingsModels {
    public class JwtSettings {
        public const string JWT = "JWT";

        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public double Expires { get; set; }
    }
}