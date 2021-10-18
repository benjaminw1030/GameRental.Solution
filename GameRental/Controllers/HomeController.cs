using Microsoft.AspNetCore.Mvc;

namespace GameRental.Controllers
{
  public class HomeController : Controller
  {
    // [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}