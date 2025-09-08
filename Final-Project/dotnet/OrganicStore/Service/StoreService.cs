using Microsoft.EntityFrameworkCore;
using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStoreApplication.Context;

namespace OrganicStore.Service
{
    public class StoreService : IStoreService
    {
        private readonly DataContext _ctx;
        public StoreService(DataContext ctx) => _ctx = ctx;

        public async Task<ServiceResponse<Store>> GetStoreAsync(int id)
        {
            var store = await _ctx.Set<Store>()
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
            return new ServiceResponse<Store>
            {
                Data = store,
                Success = store != null,
                Message = store == null ? "Store not found." : null
            };
        }

        public async Task<ServiceResponse<IEnumerable<Store>>> GetAllStoresAsync()
        {
            var stores = await _ctx.Set<Store>().ToListAsync();
            return new ServiceResponse<IEnumerable<Store>> { Data = stores };
        }

        public async Task<ServiceResponse<Product>> AddOrUpdateProductAsync(int storeId, Product product)
        {
            var store = await _ctx.Set<Store>().Include(s => s.Products).FirstOrDefaultAsync(s => s.Id == storeId);
            if (store == null) return new ServiceResponse<Product> { Success = false, Message = "Store not found." };

            if (product.Id == 0)
            {
                // New product
                // ensure storeId field exists on Product; if not, assume relationship via linking table is configured through navigation
                // Here we simply add product and assume Store -> Products relation is mapped via foreign key StoreId on Product (if present)
                store.Products.Add(product);
                _ctx.Products.Add(product);
            }
            else
            {
                _ctx.Products.Update(product);
            }
            await _ctx.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product, Message = "Product saved." };
        }

        public async Task<ServiceResponse<IEnumerable<Order>>> GetIncomingOrdersAsync(int storeId)
        {
            var orders = await _ctx.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.StoreId == storeId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return new ServiceResponse<IEnumerable<Order>> { Data = orders };
        }

        public async Task<ServiceResponse<bool>> UpdateOrderStatusAsync(int storeId, int orderId, string status)
        {
            var order = await _ctx.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.StoreId == storeId);
            if (order == null) return new ServiceResponse<bool> { Success = false, Message = "Order not found." };
            order.Status = status;
            await _ctx.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true, Message = "Status updated." };
        }
    }
}
