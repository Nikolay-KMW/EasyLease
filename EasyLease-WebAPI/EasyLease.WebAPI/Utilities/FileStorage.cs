using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace EasyLease.WebAPI.Utilities {
    public class FileStorage {
        private readonly FileStorageSettings _fileStorageSettings;

        public FileStorage(FileStorageSettings fileStorageSettings) {
            _fileStorageSettings = fileStorageSettings;
        }

        internal async Task<ICollection<Image>> SavePhotoByIdAsync(Guid id, List<IFormFile> photos) {
            string path = _fileStorageSettings.FullPath + "\\" + id.ToString();

            ICollection<Image> images = new Collection<Image>();

            this.DeletePhotosById(id);

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            dirInfo.Create();

            if (dirInfo.Exists) {
                foreach (var photo in photos) {
                    var fileExtension = Path.GetExtension(photo.FileName).ToLower();

                    var generatedFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                    var filePath = Path.Combine(path, $"{generatedFileName}{fileExtension}");

                    using (var stream = System.IO.File.Create(filePath)) {
                        await photo.CopyToAsync(stream).ConfigureAwait(false);
                    }

                    var filePathForImage = filePath[filePath.LastIndexOf(_fileStorageSettings.PathInWebRoot)..];

                    images.Add(new Image() { Name = generatedFileName, Path = filePathForImage });
                }
            }

            return images;
        }

        internal void DeletePhotosById(Guid id) {
            string path = _fileStorageSettings.FullPath + "\\" + id.ToString();

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists) {
                dirInfo.Delete(true);
            }
        }

        internal void DeletePhotoByPath(string path) {
            string fullPathForPhoto = _fileStorageSettings.WebRootPath + path;

            if (System.IO.File.Exists(fullPathForPhoto)) {
                System.IO.File.Delete(fullPathForPhoto);
            }
        }
    }
}