using OrganicStore.Model;

namespace OrganicStore.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<Order>> GetByStoreAsync(int storeId);
        Task<Order> CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task SaveChangesAsync();
    }
}
