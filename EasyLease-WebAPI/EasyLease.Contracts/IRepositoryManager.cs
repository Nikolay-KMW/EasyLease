using System.Threading.Tasks;

namespace EasyLease.Contracts {
    public interface IRepositoryManager {
        IUserRepository User { get; }
        IAdvertRepository Advert { get; }
        IComfortRepository Comfort { get; }
        ITagRepository Tag { get; }
        Task SaveAsync();
    }
}