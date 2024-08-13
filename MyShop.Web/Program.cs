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
using MyShop.Business.Services.UsersService;
using MyShop.Business.Services.ShoppingCartService;
using Stripe;
using MyShop.Business.Services.PaymentService;
using MyShop.Business.Services.OrderHeaderService;
using MyShop.Business.Services.orderDetailsService;

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
			builder.Services.Configure<StripeKeys>(builder.Configuration.GetSection("Stripe"));

			builder.Services.AddIdentity<IdentityUser,IdentityRole>(
				options => 
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(4)
				).AddDefaultUI()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddScoped<ICategoryService, CategoryServices>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IProductServices, ProductServices>();	
			builder.Services.AddScoped<IImage, ImageImplementation>();
			builder.Services.AddSingleton<IEmailSender, EmailSender>();
			builder.Services.AddScoped<IUsers, UsersRepository>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
			builder.Services.AddScoped<IShoppingCart, ShoppingCartRepository>();
			builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
			builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
			builder.Services.AddScoped<IPaymentService, PaymentServices>();
			builder.Services.AddScoped<IOrderHeaderServices, OrderHeaderServices>();
			builder.Services.AddScoped<IOrderDetailsServices, OrderDetailsServices>();
			
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

			StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:secretKey").Get<string>();

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
