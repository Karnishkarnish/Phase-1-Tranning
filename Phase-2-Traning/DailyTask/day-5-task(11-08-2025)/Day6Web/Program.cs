using Microsoft.EntityFrameworkCore;
using System;
using Day6Web.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = "Data Source=PTPLL419; Initial Catalog=TreeDB; Integrated Security=True; TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDBContext>(x => x.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "tree",
    pattern: "{controller=Tree}/{action=Index}/{id?}");

app.Run();