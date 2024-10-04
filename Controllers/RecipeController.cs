using Microsoft.AspNetCore.Mvc;
using CookBook.Models;

namespace CookBook.Controllers;

public class RecipeController : Controller {

    [HttpGet]
    public ActionResult New() {
        return View();
    }

    [HttpPost]
    public ActionResult New(Recipe model) {
        return RedirectToAction("List");
    }
}
