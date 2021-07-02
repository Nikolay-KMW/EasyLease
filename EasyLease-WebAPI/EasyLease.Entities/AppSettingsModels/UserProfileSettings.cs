using System.Collections.Generic;

namespace EasyLease.Entities.AppSettingsModels {
    public class UserProfileSettings {
        public long FileSizeLimitForAvatar { get; set; }
        public string[] AllowedExtensions { get; set; }
        public Dictionary<string, List<byte[]>> FileSignature { get; set; }
    }
}