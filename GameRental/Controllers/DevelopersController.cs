using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GameRental.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameRental.Controllers
{
  public class DevelopersController : Controller
  {
    private readonly GameRentalContext _db;

    public DevelopersController(GameRentalContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Developer> model = _db.Developers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Developer developer)
    {
      _db.Developers.Add(developer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

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
      return View(thisDeveloper);
    }

    [HttpPost]
    public ActionResult Edit(Developer developer)
    {
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
  }
}