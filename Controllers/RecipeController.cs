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

        return RedirectToAction("List", "Recipe");
    }


    public ActionResult List() {
        List<Recipe> Recipes = new List<Recipe>();

        if (HttpContext.Session.GetString("UserName") != null & ViewBag.Favorites == true) {
            var Ids = db.RecipeReads.Where(e => e.UserId == (int) HttpContext.Session.GetInt32("UserId")).Select(t => t.RecipeId).ToList();
            foreach (var id in Ids) {
                Recipes.Add((Recipe) Recipes.Where(e => e.RecipeId == id));
            }

        } else {
            Recipes = db.Recipes.ToList();
        }

        return View(Recipes);
    }


    public ActionResult Show(int id){
        ViewBag.comments = db.Comments.Where(e => e.ParentId == id).ToList();

        if (db.Recipes.Single(e => e.RecipeId == id).UserId == HttpContext.Session.GetInt32("UserId")) {
            ViewBag.Owner = true;
        }


        ViewBag.Fav = false;

        if (HttpContext.Session.GetString("UserName") != null) {
            var NN = db.RecipeReads.Single(e => e.RecipeId == id & e.UserId == (int) HttpContext.Session.GetInt32("UserId"));
            ViewBag.Fav = NN.Favorite;
        }

        return View(db.Recipes.Single(e => e.RecipeId == id));
    }


    public ActionResult CreateTable(int id) {
        return RedirectToAction("Create", "RecipeRead", new{id = id});
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
    public ActionResult Update(string Name, string Steps, int Id){
        Recipe old = db.Recipes.Single(e => e.RecipeId == Id);

        old.Name = Name;
        old.Steps = Steps;

        db.SaveChanges();

        return RedirectToAction("Show", new {id = Id});
    }


    public ActionResult Accept(int id){
        return View(id);
    }


    public ActionResult Delete(int id){
        Recipe model = db.Recipes.Single(e => e.RecipeId == id);
        db.Recipes.Remove(model);
        
        // Deleta tmb os comentarios da receita
        List<Comment> comments = db.Comments.Where(e => e.ParentId == id).ToList();

        foreach (Comment comment in comments){
            db.Comments.Remove(comment);
        }

        db.SaveChanges();

        return RedirectToAction("List");
    }


     public ActionResult Favorites() {
        ViewBag.Favorites = true;
        return RedirectToAction("List", "Recipe");
    }
}
