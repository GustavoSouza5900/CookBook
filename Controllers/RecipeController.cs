using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CookBook.Controllers;

public class RecipeController : Controller {
    private readonly CookBookDB db;


    public RecipeController(CookBookDB db) {
        this.db = db;
    }


    [HttpGet]
    public ActionResult New() {
        if (HttpContext.Session.GetInt32("UserId") != null){
            return View();
        }
        
        //else
        return RedirectToAction("Login", "User");
    }


    [HttpPost]
    public async Task<ActionResult> New(string Name, string Steps, IFormFile imagem) {

        Recipe model = new Recipe(Name, Steps, "~/Image/Default.jpg");

        if (imagem != null && imagem.Length > 0)
        {
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image", imagem.FileName);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            model.ImagePath = "~/Image/" + imagem.FileName;
        }
        
        model.UserId = (int) HttpContext.Session.GetInt32("UserId");
        model.UserName = HttpContext.Session.GetString("UserName");

        db.Recipes.Add(model);
        db.SaveChanges();

        return RedirectToAction("list");
    }


    public ActionResult List() {
        return View(db.Recipes.ToList());
    }


    public ActionResult Show(int id){
        ViewBag.comments = db.Comments.Where(e => e.ParentId == id).ToList();

        if (db.Recipes.Single(e => e.RecipeId == id).UserId == HttpContext.Session.GetInt32("UserId")) {
            ViewBag.Owner = true;
        }

        return View(db.Recipes.Single(e => e.RecipeId == id));
    }


    [HttpGet]
    public ActionResult Update(int id){
        Recipe model = db.Recipes.Single(e => e.RecipeId == id);
        
        if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") != model.UserId) {
            return RedirectToAction("Show", new {id = model.RecipeId });
        }

        return View(model);
    }


    [HttpPost]
    public ActionResult Update(Recipe model){
        Recipe old = db.Recipes.Single(e => e.RecipeId == model.RecipeId);

        old.Name = model.Name;
        old.Steps = model.Steps;

        db.SaveChanges();

        return View(model);
    }
}
