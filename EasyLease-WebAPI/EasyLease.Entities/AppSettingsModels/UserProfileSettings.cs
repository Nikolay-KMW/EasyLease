using System.Collections.Generic;

namespace EasyLease.Entities.AppSettingsModels {
    public class UserProfileSettings {
        public const string UserProfile = "UserProfile";

        public bool RequireDigitForPassword { get; set; }
        public bool RequireLowercaseForPassword { get; set; }
        public bool RequireUppercaseForPassword { get; set; }
        public bool RequireNonAlphanumericPassword { get; set; }
        public short RequiredLengthForPassword { get; set; }
        public short DefaultLockoutTimeSpanForFailedAccess { get; set; }
        public short MaxFailedAccessForSignIn { get; set; }
        public bool AllowedLockoutForNewUsers { get; set; }
        public string AllowedUserNameCharacters { get; set; }
        public long FileSizeLimitForAvatar { get; set; }
        public string[] AllowedExtensions { get; set; }
        public Dictionary<string, List<byte[]>> FileSignature { get; set; }
    }
}