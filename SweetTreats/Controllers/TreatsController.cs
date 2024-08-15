using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetTreats.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetTreats.Controllers
{
	[Authorize]
	public class SweetsController : Controller
	{
		private readonly SweetTreatsContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		
		public SweetsController(UserManager<ApplicationUser> userManager, SweetTreatsContext db)
		{
			_userManager = userManager;
			_db = db;
		}
		
		public ActionResult Index(string sort, string searchString)
		{
			ViewBag.NameSortParm = string.IsNullOrEmpty(sort) ? "name_desc" : "";
			ViewBag.PriceSortParm = sort == "price" ? "price_desc" : "price";
			
			var userSweets = from r in _db.Sweets
												select r;
			if (!string.IsNullOrEmpty(searchString))
			{
				userSweets = userSweets.Where(r => r.Name.Contains(searchString));
			}
			switch (sort)
			{
				case "name_desc":
				userSweets = userSweets.OrderByDescending(r => r.Name);
				break;
			case "price":
				userSweets = userSweets.OrderBy(r => r.Price);
				break;
			case "price_desc":
				userSweets = userSweets.OrderByDescending(r => r.Price);
				break;
			default:
				userSweets = userSweets.OrderBy(r => r.Name);
				break;
			}

			return View(userSweets.ToList());
		}
		
		public ActionResult Create()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<ActionResult> Create(Sweet sweet)
		{
			if (!ModelState.IsValid)
			{
				return View(sweet);
			}
			else
			{
				string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
				sweet.User = currentUser;
				_db.Sweets.Add(sweet);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
		}
		
		[AllowAnonymous]
		public ActionResult Details(int id)
		{
			Sweet thisSweet = _db.Sweets
				.Include(sweet => sweet.JoinEntities)
				.ThenInclude(join => join.Flavor)
				.FirstOrDefault(sweet => sweet.SweetId == id);
			return View(thisSweet);
		}
		
		public ActionResult Edit(int id)
		{
			Sweet thisSweet = _db.Sweets.FirstOrDefault(sweet => sweet.SweetId == id);
			return View(thisSweet);
		}
		
		[HttpPost]
		public ActionResult Edit(Sweet sweet)
		{
			if (!ModelState.IsValid)
			{
				return View(sweet);
			}
			else
			{
				_db.Sweets.Update(sweet);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
		}
		
		public ActionResult AddFlavor(int id)
		{
			Sweet thisSweet = _db.Sweets.FirstOrDefault(sweet => sweet.SweetId == id);
			ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
			return View(thisSweet);
		}
		
		[HttpPost]
		public ActionResult AddFlavor(Sweet sweet, int flavorId)
		{
			bool joinEntityExists = _db.SweetFlavors.Any(join => join.FlavorId == flavorId && join.SweetId == sweet.SweetId);
			if (!joinEntityExists && flavorId != 0)
			{
				_db.SweetFlavors.Add(new SweetFlavor() { FlavorId = flavorId, SweetId = sweet.SweetId });
				_db.SaveChanges();
			}
			return RedirectToAction("Details", new { id = sweet.SweetId });
		}
		
		public ActionResult Delete(int id)
		{
			Sweet thisSweet = _db.Sweets.FirstOrDefault(sweet => sweet.SweetId == id);
			return View(thisSweet);
		}
		
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
				Sweet thisSweet = _db.Sweets.FirstOrDefault(sweet => sweet.SweetId == id);
				_db.Sweets.Remove(thisSweet);
				_db.SaveChanges();
				return RedirectToAction("Index");
		}
		
		[HttpPost]
		public ActionResult DeleteJoin(int joinId)
		{
			SweetFlavor joinEntry = _db.SweetFlavors.FirstOrDefault(entry => entry.SweetFlavorId == joinId);
			_db.SweetFlavors.Remove(joinEntry);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}