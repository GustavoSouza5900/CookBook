using Microsoft.AspNetCore.Mvc;
using CookBook.Models;

namespace CookBook.Controllers;

public class CommentController : Controller {

    private readonly CookBookDB db;


    public CommentController(CookBookDB db) {
        this.db = db;
    }

    [HttpGet]
    public ActionResult Save(int id) {
        if (HttpContext.Session.GetInt32("UserId") != null){
            return View(id);
        }
        
        //else
        return RedirectToAction("Login", "User");
    }


    [HttpPost]
    public ActionResult Save(Comment model) {
        model.UserId = (int) HttpContext.Session.GetInt32("UserId");
        model.UserName = HttpContext.Session.GetString("UserName");
        db.Comments.Add(model);
        db.SaveChanges();
        return RedirectToAction("Show", "Recipe", new {id = model.ParentId});
    }
}