using OrganicStore.Model;

namespace OrganicStore.Repositories
{
    public interface IUserRepository
    {

        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
        Task AddUserAsync(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<bool> UserExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }
}
