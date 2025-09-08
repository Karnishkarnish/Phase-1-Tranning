using Microsoft.EntityFrameworkCore;
using OrganicStore.Model;


namespace OrganicStoreApplication.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================
            // Configure relationships
            // ========================

            // Order -> Customer (One customer can have many orders)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order -> Store (One store can have many orders)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItem -> Order (One order can have many order items)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem -> Product (One product can belong to many order items)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product -> Store (One store can have many products)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Store -> Owner (One user owns one store)
            modelBuilder.Entity<Store>()
                .HasOne(s => s.Owner)
                .WithOne(u => u.Store)
                .HasForeignKey<Store>(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment -> Order (One order can have one payment)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
