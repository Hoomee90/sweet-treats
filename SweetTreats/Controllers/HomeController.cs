using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SweetTreats.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetTreats.Controllers
{
	public class HomeController : Controller
	{
		private readonly SweetTreatsContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(UserManager<ApplicationUser> userManager, SweetTreatsContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		[HttpGet("/")]
		public async Task<ActionResult> Index()
		{
			
			string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
			ViewBag.items = _db.Sweets.ToList();
			ViewBag.tags = _db.Flavors.ToList();
			return View();
		}
	}
}