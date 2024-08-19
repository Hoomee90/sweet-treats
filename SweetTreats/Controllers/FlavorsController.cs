using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetTreats.Controllers
{
	[Authorize]
	public class FlavorsController : Controller
	{
		private readonly SweetTreatsContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		
		public FlavorsController(UserManager<ApplicationUser> userManager, SweetTreatsContext db)
		{
			_userManager = userManager;
			_db = db;
		}
		public ActionResult Index()
		{
			List<Flavor> flavors = _db.Flavors.ToList();
			return View(flavors);
		}
		
		[AllowAnonymous]
		public ActionResult Details(int id)
		{
			Flavor thisFlavor = _db.Flavors
				.Include(flavor => flavor.JoinEntities)
				.ThenInclude(join => join.Sweet)
				.FirstOrDefault(flavor => flavor.FlavorId == id);
			return View(thisFlavor);
		}
		
		public ActionResult Create()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<ActionResult> Create(Flavor flavor)
		{
			if (!ModelState.IsValid)
			{
				return View(flavor);
			}
			else
			{
				string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
				flavor.User = currentUser;
				_db.Flavors.Add(flavor);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
		}
		
		public ActionResult AddSweet(int id)
		{
			Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
			ViewBag.SweetId = new SelectList(_db.Sweets, "SweetId", "Name");
			return View(thisFlavor);
		}
		
		[HttpPost]
		public ActionResult AddSweet(Flavor flavor, int sweetId)
		{
			bool joinEntityExists = _db.SweetFlavors.Any(join => join.SweetId == sweetId && join.FlavorId == flavor.FlavorId);
			if (!joinEntityExists && sweetId != 0)
			{
				_db.SweetFlavors.Add(new SweetFlavor() { SweetId = sweetId, FlavorId = flavor.FlavorId });
				_db.SaveChanges();
			}
			return RedirectToAction("Details", new { id = flavor.FlavorId});
		}
		
		public ActionResult Edit(int id)
		{
			Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
			return View(thisFlavor);
		}
		
		[HttpPost]
		public ActionResult Edit(Flavor flavor)
		{
			_db.Flavors.Update(flavor);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		
		public ActionResult Delete(int id)
		{
			Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
			return View(thisFlavor);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
			_db.Flavors.Remove(thisFlavor);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		
		[HttpPost]
		public ActionResult DeleteJoin(int joinId, int flavorId)
		{
			SweetFlavor joinEntry = _db.SweetFlavors.FirstOrDefault(entry => entry.SweetFlavorId == joinId);
			_db.SweetFlavors.Remove(joinEntry);
			_db.SaveChanges();
			return RedirectToAction("Details", new { id = flavorId });
		}
	}
}