namespace EasyLease.Entities.AppSettingsModels {
    public class GeneralSettings {
        public const string General = "General";

        public GeneralSettings(string originUIUrl, short hoursOffsetForUkraine, short pageSize, short maxPageSize) {
            OriginUIUrl = originUIUrl;
            HoursOffsetForUkraine = hoursOffsetForUkraine;
            PageSize = pageSize;
            MaxPageSize = maxPageSize;
        }

        public string OriginUIUrl { get; }
        public short HoursOffsetForUkraine { get; }
        public static short PageSize { get; private set; }
        public static short MaxPageSize { get; private set; }
    }
}