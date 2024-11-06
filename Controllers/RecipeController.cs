using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using System.IO;
using System.Threading.Tasks;

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
        db.Recipes.Add(model);
        db.SaveChanges();

        model = db.Recipes.Single(e => e.Name == Name);

        if (imagem != null && imagem.Length > 0)
        {
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image", imagem.FileName);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            model.ImagePath = "~/Image/" + imagem.FileName;

            db.SaveChanges();
        }
        
        return RedirectToAction("list");
    }


    public ActionResult List() {
        return View(db.Recipes.ToList());
    }


    public ActionResult Show(int id){
        ViewBag.comments = db.Comments.Where(e => e.ParentId == id).ToList();
        return View(db.Recipes.Single(e => e.RecipeId == id));
    }
}
