namespace EasyLease.Contracts {
    public interface IRepositoryManager {
        IUserRepository User { get; }
        IAdvertRepository Advert { get; }
        IComfortRepository Comfort { get; }
        ITagRepository Tag { get; }
        void Save();
    }
}