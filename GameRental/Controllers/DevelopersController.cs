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
  public class DevelopersController : Controller
  {
    private readonly GameRentalContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public DevelopersController(UserManager<ApplicationUser> userManager, GameRentalContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Developer> model = _db.Developers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.GameId = new SelectList(_db.Games, "GameId", "Title");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Developer developer)
    {
      _db.Developers.Add(developer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisDeveloper = _db.Developers
          .Include(developer => developer.JoinEntities)
          .ThenInclude(join => join.Game)
          .FirstOrDefault(Developer => Developer.DeveloperId == id);
      return View(thisDeveloper);
    }

    public ActionResult Edit(int id)
    {
      var thisDeveloper = _db.Developers.FirstOrDefault(developer => developer.DeveloperId == id);
      ViewBag.GameId = new SelectList(_db.Games, "GameId", "Title");
      return View(thisDeveloper);
    }

    [HttpPost]
    public ActionResult Edit(Developer developer, int GameId)
    {
      if (GameId != 0)
      {
        _db.DeveloperGame.Add(new DeveloperGame() { DeveloperId = developer.DeveloperId, GameId = GameId});
      }
      _db.Entry(developer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisDeveloper = _db.Developers.FirstOrDefault(developer => developer.DeveloperId == id);
      return View(thisDeveloper);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisDeveloper = _db.Developers.FirstOrDefault(developer => developer.DeveloperId == id);
      _db.Developers.Remove(thisDeveloper);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddGame(int id)
    {
      var thisDeveloper = _db.Developers.FirstOrDefault(developer => developer.DeveloperId == id);
      List<Game> gameDevelopers = new List<Game> { };
      foreach (DeveloperGame join in thisDeveloper.JoinEntities)
      {
        gameDevelopers.Add(join.Game);
      }
      var gameSelect = _db.Games.Where(game => !gameDevelopers.Contains(game)).ToList();
      ViewBag.GameId = new SelectList(gameSelect, "GameId", "Title");
      ViewBag.ValidGames = gameSelect;
      return View(thisDeveloper);
    }

    [HttpPost]
    public ActionResult AddGame(Developer developer, int GameId)
    {
      if (GameId != 0)
      {
        _db.DeveloperGame.Add(new DeveloperGame() { DeveloperId =developer.DeveloperId, GameId = GameId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteGame(int joinId)
    {
      var joinEntry = _db.DeveloperGame.FirstOrDefault(entry => entry.DeveloperGameId == joinId);
      _db.DeveloperGame.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Search(string search)
    {
      char[] charsToTrim = { ' ' };
      string search2 = search.ToLower().Trim(charsToTrim);
      var foundDevelopers = _db.Developers.Where(developer => developer.Name.ToLower().Contains(search2)).ToList();
      return View(foundDevelopers);
    }
  }
}