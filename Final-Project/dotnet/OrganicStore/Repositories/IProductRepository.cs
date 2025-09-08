using OrganicStore.Model;

namespace OrganicStore.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
        Task<IEnumerable<Product>> GetProductsByStoreAsync(int storeId);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task AddProductAsync(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<bool> ProductExistsAsync(int id);
    }
}
