using OrganicStore.Dtos;
using OrganicStore.Model;

namespace OrganicStore.Service
{
    public interface IStoreService
    {
        Task<ServiceResponse<Store>> GetStoreAsync(int id);
        Task<ServiceResponse<IEnumerable<Store>>> GetAllStoresAsync();
        Task<ServiceResponse<Product>> AddOrUpdateProductAsync(int storeId, Product product);
        Task<ServiceResponse<IEnumerable<Order>>> GetIncomingOrdersAsync(int storeId);
        Task<ServiceResponse<bool>> UpdateOrderStatusAsync(int storeId, int orderId, string status);
    }
}
