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
  public class GamesController : Controller
  {
    private readonly GameRentalContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public GamesController(UserManager<ApplicationUser> userManager, GameRentalContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    // public async Task<ActionResult> Index()
    // {
    //   var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   var currentUser = await _userManager.FindByIdAsync(userId);
    //   var userGames = _db.Games.Where(entry => entry.User.Id == currentUser.Id).ToList();
    //   return View(userGames);
    // }
    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Game> model = _db.Games.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.DeveloperId = new SelectList(_db.Developers, "DeveloperId", "Name");
      return View();
    }

    // [HttpPost]
    // public async Task<ActionResult> Create(Game game, int DeveloperId)
    // {
    //   var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   var currentUser = await _userManager.FindByIdAsync(userId);
    //   game.User = currentUser;
    //   _db.Games.Add(game);
    //   _db.SaveChanges();
    //   if (DeveloperId != 0)
    //   {
    //     _db.DeveloperGame.Add(new DeveloperGame() { DeveloperId = DeveloperId, GameId = game.GameId });
    //   }
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    [HttpPost]
    public ActionResult Create(Game game)
    {
      _db.Games.Add(game);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisGame = _db.Games
        .Include(game => game.JoinEntities)
        .ThenInclude(join => join.Developer)
        .FirstOrDefault(game => game.GameId == id);
      ViewBag.CopyToRent = _db.Copies.FirstOrDefault(copy => copy.Rented == false && copy.GameId == thisGame.GameId);
      return View(thisGame);
    }

    public ActionResult Edit(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    [HttpPost]
    public ActionResult Edit(Game game, int DeveloperId)
    {
      if (DeveloperId != 0)
      {
        _db.DeveloperGame.Add(new DeveloperGame() { DeveloperId = DeveloperId, GameId = game.GameId });
      }
      _db.Entry(game).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      _db.Games.Remove(thisGame);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddDeveloper(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      List<Developer> developerGames = new List<Developer> { };
      foreach (DeveloperGame join in thisGame.JoinEntities)
      {
        developerGames.Add(join.Developer);
      }
      var developerSelect = _db.Developers.Where(developer => !developerGames.Contains(developer)).ToList();
      ViewBag.DeveloperId = new SelectList(developerSelect, "DeveloperId", "Name");
      ViewBag.ValidDevelopers = developerSelect;
      return View(thisGame);
    }

    [HttpPost]
    public ActionResult AddDeveloper(Game game, int DeveloperId)
    {
      if (DeveloperId != 0)
      {
        _db.DeveloperGame.Add(new DeveloperGame() { DeveloperId = DeveloperId, GameId = game.GameId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteDeveloper(int joinId)
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
      var foundGames = _db.Games.Where(game => game.Title.ToLower().Contains(search2)).ToList();
      return View(foundGames);
    }
  }
}