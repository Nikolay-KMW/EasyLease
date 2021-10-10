using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.DataTransferObjects;
using Microsoft.Extensions.Caching.Memory;

namespace EasyLease.WebAPI.Services {
    public class AdditionalDataService {
        private readonly IRepositoryManager _repository;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly AdvertSettings _adSettings;

        public AdditionalDataService(IRepositoryManager repository, IMemoryCache memoryCache, IMapper mapper, AdvertSettings adSettings) {
            _repository = repository;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _adSettings = adSettings;
        }

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public async Task<AdvertAdditionalDataDTO> GetAdditionalDataForAdvertAsync() {
            AdvertAdditionalDataDTO advertAdditionalData;

            if (!_memoryCache.TryGetValue(nameof(AdvertAdditionalDataDTO), out advertAdditionalData)) {
                await _lock.WaitAsync().ConfigureAwait(false);

                try {
                    if (!_memoryCache.TryGetValue(nameof(AdvertAdditionalDataDTO), out advertAdditionalData)) {
                        var additionalDataIDTO = new AdvertAdditionalDataIDTO {
                            AdvertType = await _repository.AdvertType.GetAllAdvertTypeAsync(trackChanges: false).ConfigureAwait(false),
                            SettlementType = await _repository.SettlementType.GetAllSettlementTypeAsync(trackChanges: false).ConfigureAwait(false),
                            StreetType = await _repository.StreetType.GetAllStreetTypeAsync(trackChanges: false).ConfigureAwait(false),
                            Locations = await _repository.Location.GetAllLocationAsync(trackChanges: false).ConfigureAwait(false),
                            Comforts = await _repository.Comfort.GetAllComfortsAsync(trackChanges: false).ConfigureAwait(false)
                        };

                        advertAdditionalData = _mapper.Map<AdvertAdditionalDataDTO>(additionalDataIDTO);

                        _memoryCache.Set(nameof(AdvertAdditionalDataDTO), advertAdditionalData, TimeSpan.FromHours(_adSettings.CachingTimeForAdditionalDate));
                    }
                } finally {
                    _lock.Release();
                }
            }

            return advertAdditionalData;
        }
    }
}