using System.Threading.Tasks;
using EasyLease.Entities.DataTransferObjects;

namespace EasyLease.Contracts {
    public interface IAuthenticationManager {
        Task<bool> ValidateUser(UserAuthenticationDTO userAuth);
        Task<string> CreateToken();
    }
}