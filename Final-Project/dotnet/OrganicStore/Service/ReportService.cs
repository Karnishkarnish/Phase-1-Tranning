using Microsoft.EntityFrameworkCore;
using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStoreApplication.Context;

namespace OrganicStore.Service
{
    public class ReportService : IReportService
    {
        private readonly DataContext _ctx;

        public ReportService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ServiceResponse<IEnumerable<SalesBucketDto>>> GetSalesAsync(DateTime from, DateTime to, string bucket)
        {
            var response = new ServiceResponse<IEnumerable<SalesBucketDto>>();

            var query = _ctx.Orders
                .Where(o => o.CreatedAt >= from && o.CreatedAt <= to);

            List<SalesBucketDto> buckets;

            switch (bucket.ToLower())
            {
                case "day":
                    buckets = await query
                        .GroupBy(o => o.CreatedAt.Date)
                        .Select(g => new SalesBucketDto
                        {
                            Bucket = g.Key.ToString("yyyy-MM-dd"),
                            TotalSales = g.Sum(x => x.TotalAmount),
                            Orders = g.Count()
                        })
                        .ToListAsync();
                    break;

                case "month":
                    buckets = await query
                        .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
                        .Select(g => new SalesBucketDto
                        {
                            Bucket = $"{g.Key.Year}-{g.Key.Month:D2}",
                            TotalSales = g.Sum(x => x.TotalAmount),
                            Orders = g.Count()
                        })
                        .ToListAsync();
                    break;

                default: // week
                    buckets = await query
                        .GroupBy(o => EF.Functions.DateDiffWeek(DateTime.MinValue, o.CreatedAt))
                        .Select(g => new SalesBucketDto
                        {
                            Bucket = $"Week {g.Key}",
                            TotalSales = g.Sum(x => x.TotalAmount),
                            Orders = g.Count()
                        })
                        .ToListAsync();
                    break;
            }

            response.Data = buckets;
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<TopProductDto>>> GetTopProductsAsync(DateTime from, DateTime to, int top = 10)
        {
            var response = new ServiceResponse<IEnumerable<TopProductDto>>();

            var products = await _ctx.OrderItems
                .Where(oi => oi.Order.CreatedAt >= from && oi.Order.CreatedAt <= to)
                .GroupBy(oi => oi.ProductId)
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key,
                    Name = g.First().Product.Name,
                    Count = g.Sum(x => x.Quantity),
                    Sales = g.Sum(oi => oi.Price * oi.Quantity)
                })
                .OrderByDescending(x => x.Sales)
                .Take(top)
                .ToListAsync();

            response.Data = products;
            return response;
        }

        public async Task<ServiceResponse<DashboardSummaryDto>> GetDashboardAsync()
        {
            var response = new ServiceResponse<DashboardSummaryDto>();

            var summary = new DashboardSummaryDto
            {
                Customers = await _ctx.Users.CountAsync(u => u.Role == "Customer"),
                Stores = await _ctx.Stores.CountAsync(),
                Orders = await _ctx.Orders.CountAsync(),
                Sales = await _ctx.Orders.SumAsync(o => o.TotalAmount)
            };

            response.Data = summary;
            return response;
        }
    }
}
