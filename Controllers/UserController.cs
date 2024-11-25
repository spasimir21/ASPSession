using System.Net;
using BorrowLend.Data;
using Microsoft.AspNetCore.Mvc;

namespace BorrowLend.Controllers
{
  public class UserController : Controller
  {
    private readonly ApplicationDbContext _db;
    public UserController(ApplicationDbContext db)
    {
      _db = db;
    }

    [HttpPost]
    public IActionResult SetUserName(string userName)
    {
      HttpContext.Session.SetString("UserName", userName);
      return RedirectToAction("Index");
    }

    public IActionResult SetCookie(string preference)
    {
      Response.Cookies.Append("UserPreference", preference, new CookieOptions
      {
        Expires = DateTimeOffset.Now.AddDays(7),
        HttpOnly = true,
        IsEssential = true
      });

      return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
      if (Request.Cookies.TryGetValue("UserPreference", out string userPreference))
      {
        ViewBag.UserPreference = userPreference;
      }
      else
      {
        ViewBag.UserPreference = "Default";
      }

      ViewBag.UserName = HttpContext.Session.GetString("UserName");
      return View();
    }
  }
}
