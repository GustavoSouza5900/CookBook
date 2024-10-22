using Microsoft.AspNetCore.Mvc;
using CookBook.Models;

namespace CookBook.Controllers;

public class CommentController : Controller {

    private readonly CookBookDB db;


    public CommentController(CookBookDB db) {
        this.db = db;
    }

    [HttpGet]
    public ActionResult Save() {
        return View();
    }


    [HttpPost]
    public void Save(Comment model) {
        db.Comments.Add(model);
        db.SaveChanges();
    }

    public ActionResult Show() {
        return View(db.Comments.ToList()); // Termina saporra (filtragem para exibir apenas comentarios da receita)
    }
}