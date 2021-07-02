using System;
using System.Collections.Generic;

namespace EasyLease.Entities.AppSettingsModels {
    public class FileStorageSettings {
        public string WebRootPath { get; set; }
        public string PathInWebRoot { get; set; }
        public long FileSizeLimit { get; set; }
        public short NumberOfFilesLimit { get; set; }
        public string[] AllowedExtensions { get; set; }
        public Dictionary<string, List<byte[]>> FileSignature { get; set; }

        public string FullPath { get => WebRootPath + PathInWebRoot; }
    }
}