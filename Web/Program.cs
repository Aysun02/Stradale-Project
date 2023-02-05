using Core.Entities;
using Core.Utilities.FileService;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.Services.Concrete;
using Web.Services.Abstract;
using Web.Services.Concrete;
using WebApp.Services.Abstract;
using WebApp.Services.Concrete;

using AdminAbstractService = Web.Areas.Admin.Services.Abstract;
using AbstractService = Web.Services.Abstract;
using AdminConcreteService = Web.Areas.Admin.Services.Concrete;
using ConcreteService = Web.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

#region Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x =>
x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));

#endregion

#region UserIdentity

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>();

#endregion

#region Repositories

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
builder.Services.AddScoped<IAdditionRepository, AdditionRepository>();
builder.Services.AddScoped<IWiseExpressionRepository, WiseExpressionRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IBlogPhotoRepository, BlogPhotoRepository>();
builder.Services.AddScoped<IGalleryElementRepository, GalleryElementRepository>();
builder.Services.AddScoped<IGalleryPhotoRepository, GalleryPhotoRepository>();
builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<IHomeMainSliderPhotoRepsository, HomeMainSliderPhotoRepository>();
builder.Services.AddScoped<IHomeGalleryRepository, HomeGalleryRepository>();
builder.Services.AddScoped<IHomeGalleryPhotoRepository, HomeGalleryPhotoRepository>();
builder.Services.AddScoped<IInformationRepository, InformationRepository>();
builder.Services.AddScoped<IInfoPhotoRepository, InfoPhotoRepository>();
builder.Services.AddScoped<IOfferPhotoRepository, OfferPhotoRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();

#endregion

#region Services
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IGalleryElementService, GalleryElementService>();
builder.Services.AddScoped<AbstractService.IAccountService, ConcreteService.AccountService>();
builder.Services.AddScoped<AdminAbstractService.IAccountService, AdminConcreteService.AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<IAdditionService, AdditionService>();
builder.Services.AddScoped<IWiseExpressionService, WiseExpressionService>();    
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IHomeMainSliderService, HomeMainSliderService>();
builder.Services.AddScoped<IHomeGalleryService, HomeGalleryService>();
builder.Services.AddScoped<IInformationService, InformationService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddSingleton<IFileService, FileService>();


#endregion

#region App

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

#endregion

#region Route

app.MapControllerRoute(
    name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
   

app.Run();

#endregion
