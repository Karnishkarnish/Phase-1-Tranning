using Microsoft.EntityFrameworkCore;
using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStoreApplication.Context;

namespace OrganicStore.Service
{
    public class ProductService : IProductService
    {
        private readonly DataContext _ctx;

        public ProductService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ServiceResponse<IEnumerable<Product>>> GetAllProductsAsync()
        {
            var products = await _ctx.Products
                .Include(p => p.Store)
                .Where(p => p.IsAvailable) // only show available products
                .ToListAsync();

            return new ServiceResponse<IEnumerable<Product>> { Data = products, Success = true };
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
        {
            var product = await _ctx.Products
                .Include(p => p.Store)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return new ServiceResponse<Product> { Success = false, Message = "Product not found" };

            return new ServiceResponse<Product> { Data = product, Success = true };
        }

        public async Task<ServiceResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(string category)
        {
            var products = await _ctx.Products
                .Where(p => p.Category == category && p.IsAvailable)
                .ToListAsync();

            return new ServiceResponse<IEnumerable<Product>> { Data = products, Success = true };
        }

        public async Task<ServiceResponse<IEnumerable<Product>>> SearchProductsAsync(string searchTerm)
        {
            var products = await _ctx.Products
                .Where(p => p.Name.Contains(searchTerm) && p.IsAvailable)
                .ToListAsync();

            return new ServiceResponse<IEnumerable<Product>> { Data = products, Success = true };
        }

        public async Task<ServiceResponse<IEnumerable<Product>>> GetProductsByStoreAsync(int storeId)
        {
            var products = await _ctx.Products
                .Where(p => p.StoreId == storeId && p.IsAvailable)
                .ToListAsync();

            return new ServiceResponse<IEnumerable<Product>> { Data = products, Success = true };
        }

        public async Task<ServiceResponse<Product>> CreateProductAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            _ctx.Products.Add(product);
            await _ctx.SaveChangesAsync();

            return new ServiceResponse<Product>
            {
                Data = product,
                Success = true,
                Message = "Product created successfully"
            };
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _ctx.Products.FindAsync(dto.Id);
            if (product == null)
                return new ServiceResponse<Product> { Success = false, Message = "Product not found" };

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.Category = dto.Category;
            product.ImageUrl = dto.ImageUrl;
            product.IsAvailable = dto.IsAvailable;
            product.UpdatedAt = DateTime.UtcNow;

            await _ctx.SaveChangesAsync();

            return new ServiceResponse<Product>
            {
                Data = product,
                Success = true,
                Message = "Product updated successfully"
            };
        }

        // 🔹 Soft Delete Implementation
        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var product = await _ctx.Products.FindAsync(id);
            if (product == null)
                return new ServiceResponse<bool> { Success = false, Message = "Product not found" };

            // Instead of deleting, mark as unavailable
            product.IsAvailable = false;
            product.UpdatedAt = DateTime.UtcNow;

            await _ctx.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Product marked as unavailable (soft delete)"
            };
        }
    }
}
