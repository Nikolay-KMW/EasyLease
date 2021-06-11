namespace EasyLease.Contracts {
    public interface IRepositoryManager {
        IUserRepository User { get; }
        IAdvertRepository Advert { get; }
        ITagRepository Tag { get; }
        void Save();
    }
}