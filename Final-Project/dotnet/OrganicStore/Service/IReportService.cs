using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStoreApplication.Context;

namespace OrganicStore.Service
{
    public interface IReportService
    {
        Task<ServiceResponse<IEnumerable<SalesBucketDto>>> GetSalesAsync(DateTime from, DateTime to, string bucket); // week, month, day
        Task<ServiceResponse<IEnumerable<TopProductDto>>> GetTopProductsAsync(DateTime from, DateTime to, int top = 10);
        Task<ServiceResponse<DashboardSummaryDto>> GetDashboardAsync();
    }

    public class SalesBucketDto
    {
        public string Bucket { get; set; }
        public decimal TotalSales { get; set; }
        public int Orders { get; set; }
    }

    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Sales { get; set; }
    }

    public class DashboardSummaryDto
    {
        public int Customers { get; set; }
        public int Stores { get; set; }
        public int Orders { get; set; }
        public decimal Sales { get; set; }
    }
}
   

