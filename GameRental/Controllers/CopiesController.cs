using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GameRental.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace GameRental.Controllers
{
  [Authorize]
  public class CopiesController : Controller
  {
    private readonly GameRentalContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public CopiesController(UserManager<ApplicationUser> userManager, GameRentalContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Create()
    {
      ViewBag.GameId = new SelectList(_db.Games, "GameId", "Title");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Copy copy, int copies)
    {
      _db.Copies.Add(copy);
      _db.SaveChanges();
      for (int i = 1; i < copies; i++)
      {
        copy.CopyId++;
        _db.Copies.Add(copy);
        _db.SaveChanges();
      }
      return RedirectToAction("Index", "Games");
    }

    [HttpPost]
    public async Task<ActionResult> Rent(Copy copy)
    {
      _db.Entry(copy).State = EntityState.Modified; 
      _db.SaveChanges();
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if (copy.CopyId !=0)
      {
        _db.Checkouts.Add(new Checkout() { CopyId = copy.CopyId, UserId = currentUser.Id });
      }
      _db.SaveChanges();
      return RedirectToAction("Index", "Games");
    }
  }
}