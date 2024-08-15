using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SweetTreats.Models;
using Microsoft.AspNetCore.Identity;

namespace SweetTreats
{
	class Program
	{
		static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();
			
			builder.Services.AddDbContext<SweetTreatsContext>(
											dbContextOptions => dbContextOptions
												.UseMySql(
													builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"])
												)
											);
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
											.AddEntityFrameworkStores<SweetTreatsContext>()
											.AddDefaultTokenProviders();

			WebApplication app = builder.Build();
			
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			
			app.UseRouting();
			
			app.UseAuthentication();
			app.UseAuthorization();
			
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}"
			);

			app.Run();
		}
	}
}