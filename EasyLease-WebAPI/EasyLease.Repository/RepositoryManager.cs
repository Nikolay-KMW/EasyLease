using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities;

namespace EasyLease.Repository {
    public class RepositoryManager : IRepositoryManager {
        private readonly RepositoryContext _repositoryContext;
        private IUserRepository _userRepository;
        private IAdvertRepository _advertRepository;
        private IComfortRepository _comfortRepository;
        private ITagRepository _tagRepository;
        private RealtyTypeRepository _realtyTypeRepository;
        private SettlementTypeRepository _settlementTypeRepository;
        private StreetTypeRepository _streetTypeRepository;
        private LocationRepository _LocationRepository;

        public RepositoryManager(RepositoryContext repositoryContext) {
            _repositoryContext = repositoryContext;
        }

        public IUserRepository User => _userRepository ??= new UserRepository(_repositoryContext);
        public IAdvertRepository Advert => _advertRepository ??= new AdvertRepository(_repositoryContext);
        public IComfortRepository Comfort => _comfortRepository ??= new ComfortRepository(_repositoryContext);
        public ITagRepository Tag => _tagRepository ??= new TagRepository(_repositoryContext);
        public IRealtyTypeRepository RealtyType => _realtyTypeRepository ??= new RealtyTypeRepository(_repositoryContext);
        public ISettlementTypeRepository SettlementType => _settlementTypeRepository ??= new SettlementTypeRepository(_repositoryContext);
        public IStreetTypeRepository StreetType => _streetTypeRepository ??= new StreetTypeRepository(_repositoryContext);
        public ILocationRepository Location => _LocationRepository ??= new LocationRepository(_repositoryContext);

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}