using OrganicStore.Dtos;
using OrganicStore.Model;

namespace OrganicStore.Service
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<Product>>> GetAllProductsAsync();
        Task<ServiceResponse<Product>> GetProductByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(string category);
        Task<ServiceResponse<IEnumerable<Product>>> SearchProductsAsync(string searchTerm);
        Task<ServiceResponse<Product>> CreateProductAsync(Product product);
        Task<ServiceResponse<Product>> UpdateProductAsync(UpdateProductDto dto);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);

        // 🔹 Added new method
        Task<ServiceResponse<IEnumerable<Product>>> GetProductsByStoreAsync(int storeId);
    }
}
