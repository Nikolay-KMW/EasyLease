namespace EasyLease.WebAPI {
    public class FileStorageSettings {
        public string PathInWebRoot { get; set; }
        public long FileSizeLimit { get; set; }
        public string[] AllowedExtensions { get; set; }
    }
}