using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SweetTreats.Models
{
	public class SweetTreatsContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Sweet> Sweets { get; set; }
		public DbSet<Flavor> Flavors { get; set; }
		public DbSet<SweetFlavor> SweetFlavors { get; set; }

		public SweetTreatsContext(DbContextOptions options) : base(options) { }
	}
}