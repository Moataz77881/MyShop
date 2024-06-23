using Microsoft.EntityFrameworkCore;
using MyShop.Business.Services.CategoryService;
using MyShop.Business.Services.ImageService;
using MyShop.Business.Services.ProductService;
using MyShop.DataAccess.Implementation;
using MyShop.DataAccess.Repository;
using MyShop.Web.Data;
using Microsoft.AspNetCore.Identity;
using MyShop.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MyShop.Web
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
			builder.Services.AddDbContext<ApplicationDbContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyShopDatabase"))
			);

			builder.Services.AddIdentity<IdentityUser,IdentityRole>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddScoped<ICategoryService, CategoryServices>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IProductServices, ProductServices>();
			builder.Services.AddScoped<IImage, ImageImplementation>();
			builder.Services.AddSingleton<IEmailSender, EmailSender>();
			
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

			app.MapRazorPages();

            app.MapControllerRoute(
				name: "Cudtomer",
				pattern: "{area=Customer}/{controller=Item}/{action=getAllitems}/{id?}");

            app.MapControllerRoute(
				name: "Admin",
				pattern: "{area=Admin}/{controller=Category}/{action=getCategories}/{id?}");


            app.Run();
		}
	}
}
