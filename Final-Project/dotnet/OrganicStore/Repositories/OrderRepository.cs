using Microsoft.EntityFrameworkCore;
using OrganicStore.Model;
using OrganicStoreApplication.Context;

namespace OrganicStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _ctx;
        public OrderRepository(DataContext ctx) => _ctx = ctx;

        public async Task<Order> GetByIdAsync(int id) =>
            await _ctx.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<Order>> GetByCustomerAsync(int customerId) =>
            await _ctx.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

        public async Task<IEnumerable<Order>> GetByStoreAsync(int storeId) =>
            await _ctx.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.StoreId == storeId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

        public async Task<Order> CreateAsync(Order order)
        {
            _ctx.Orders.Add(order);
            await _ctx.SaveChangesAsync();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _ctx.Orders.Update(order);
            await _ctx.SaveChangesAsync();
        }

        public Task SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
