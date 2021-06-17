using EasyLease.Contracts;
using EasyLease.Entities;

namespace EasyLease.Repository {
    public class RepositoryManager : IRepositoryManager {
        private readonly RepositoryContext _repositoryContext;
        private IUserRepository _userRepository;
        private IAdvertRepository _advertRepository;
        private ILocationRepository _locationRepository;
        private IComfortRepository _comfortRepository;
        private ITagRepository _tagRepository;

        public RepositoryManager(RepositoryContext repositoryContext) {
            _repositoryContext = repositoryContext;
        }

        public IUserRepository User => _userRepository ??= new UserRepository(_repositoryContext);
        public IAdvertRepository Advert => _advertRepository ??= new AdvertRepository(_repositoryContext);
        public ILocationRepository Location => _locationRepository ??= new LocationRepository(_repositoryContext);
        public IComfortRepository Comfort => _comfortRepository ??= new ComfortRepository(_repositoryContext);
        public ITagRepository Tag => _tagRepository ??= new TagRepository(_repositoryContext);

        public void Save() => _repositoryContext.SaveChanges();
    }
}