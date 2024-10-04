using Microsoft.AspNetCore.Mvc;
using CookBook.Models;

namespace CookBook.Controllers;

public class RecipeController : Controller {
    private readonly CookBookDB db;


    public RecipeController(CookBookDB db) {
        this.db = db;
    }


    [HttpGet]
    public ActionResult New() {
        return View();
    }


    [HttpPost]
    public ActionResult New(Recipe model) {
        db.Recipes.Add(model);
        db.SaveChanges();
        return RedirectToAction("list");
    }


    public ActionResult List() {
        return View(db.Recipes.ToList());
    }
}
