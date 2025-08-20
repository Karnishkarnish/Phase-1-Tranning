
using Day12api.Context;
using Microsoft.EntityFrameworkCore;

namespace Day12api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            const string connectionString = "Data Source=PTPLL419;" +
                                "Initial Catalog=TestDB;" +
                                "Integrated Security=True;" +
                                "TrustServerCertificate=True;";
            builder.Services.AddDbContext<MyAppDbContext>(options =>
    options.UseSqlServer(connectionString));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
