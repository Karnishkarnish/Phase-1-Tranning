using OrganicStore.Dtos;
using OrganicStore.Model;

namespace OrganicStore.Service
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
    }
}
