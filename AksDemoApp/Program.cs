using AksDemoApp.Data;
using AksDemoApp.Models;
using AksDemoApp.Models.Category;
using AksDemoApp.Models.Products;
using AksDemoApp.Models.SubCategory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connstr = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connstr), ServiceLifetime.Singleton
);

builder.Services.AddScoped<IAdminRepo , AdminRepo>();
builder.Services.AddSingleton<ICategoryRepo, CategoryRepo>();
builder.Services.AddSingleton<ISubCategoryRepo, SubCategoryRepo>();
builder.Services.AddSingleton<IProductRepo, ProductRepo>();

var app = builder.Build(); 


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
