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
using System;

namespace GameRental.Controllers
{
  public class HomeController : Controller
  {
    private readonly GameRentalContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public HomeController(UserManager<ApplicationUser> userManager, GameRentalContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Overdue()
    {
      TimeSpan rentalTime = new TimeSpan(-7, 0, 0, 0);
      DateTime dueDate = DateTime.Now.Add(rentalTime);
      var overdueGames = _db.Checkouts
      .ToList()
      .AsEnumerable()
      .Where(checkout => dueDate.Ticks > checkout.Copy.RentalDate.Ticks)
      .ToList();
      return View(overdueGames);
    }
  }
}