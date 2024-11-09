using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CookBook.Controllers;


public class RecipeReadController : Controller {
    private readonly CookBookDB db;

    public RecipeReadController(CookBookDB db) {
        this.db = db;
    }


    public ActionResult Create (int Id) {
        if (HttpContext.Session.GetString("UserName") != null) {
            if(db.RecipeReads.SingleOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId") & e.RecipeId == Id) == null) {
                RecipeRead rr = new RecipeRead(Id,(int) HttpContext.Session.GetInt32("UserId"), db.Recipes.Single(e => e.RecipeId == Id), db.Users.Single(e => e.UserId == (int)HttpContext.Session.GetInt32("UserId")));
                
                rr.Favorite = false;
                rr.StarsRating = 0;

                db.RecipeReads.Add(rr);
                db.SaveChanges();
            }
        }
        
        return RedirectToAction("Show", "Recipe", new {id = Id});
    }


    public ActionResult Favorite(int id){
        if (HttpContext.Session.GetString("UserName") != null) {
            RecipeRead model = db.RecipeReads.Single(e => e.RecipeId == id & e.UserId == (int) HttpContext.Session.GetInt32("UserId"));
            model.Favorite = !model.Favorite;

            Recipe Recipe = db.Recipes.Single(e => e.RecipeId == model.RecipeId);

            if (model.Favorite) {
                Recipe.FavCount += 1;
            } else {
                Recipe.FavCount -= 1;
            }

            db.SaveChanges();
        }
        return RedirectToAction("Show", "Recipe", new { id = id });
    }


    [HttpPost]
    public ActionResult Star(int id, int Stars){
        RecipeRead model = db.RecipeReads.Single(e => e.RecipeId == id & e.UserId == (int) HttpContext.Session.GetInt32("UserId"));
        model.StarsRating = Stars;

        var Ratings = db.RecipeReads.Where(e => e.RecipeId == id).ToList();
        Recipe Recipe = db.Recipes.Single(e => e.RecipeId == id);

        int count = 0;
        int sum = 0;
        foreach (var Rate in Ratings) {
            if (Rate.StarsRating != 0) {
                count++;
                sum += Rate.StarsRating;
            }
        }

        Recipe.Stars = sum / (float) count;

        db.SaveChanges();

        return RedirectToAction("Show", "Recipe", new { id = id });
    }
}