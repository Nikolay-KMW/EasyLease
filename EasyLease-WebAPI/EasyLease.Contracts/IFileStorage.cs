using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyLease.Entities.Models;

namespace EasyLease.Contracts {
    public interface IFileStorage {
        Task<ICollection<Image>> SavePhotoByIdAsync<TFile>(Guid id, IList<TFile> photos);
        void DeletePhotosById(Guid id);
        void DeletePhotoByPath(string path);
    }
}