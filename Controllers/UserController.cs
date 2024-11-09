using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace CookBook.Controllers;

    public class UserController : Controller {
        private readonly CookBookDB db;

        public UserController(CookBookDB db) {
            this.db = db;
        }


        [HttpGet]
        public ActionResult Register() {
            return View();
        }


        [HttpPost]
        public ActionResult Register(User model) {
            if (db.Users.FirstOrDefault(e => e.Email == model.Email)!= null){
                ViewBag.Exist = true;
                return View(model);
            }
            if (ModelState.IsValid) {
                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Login() {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserViewModel model) {
    
            var usr = db.Users.FirstOrDefault(e => e.Email == model.Email && e.Password == model.Password);
            
            if (usr == null) {
                ViewBag.Auth = false;
                return View(model);
            }

            //else
            ViewBag.Auth = true;
            HttpContext.Session.SetInt32("UserId", usr.UserId);
            HttpContext.Session.SetString("UserName", usr.Name);

            return RedirectToAction("List", "Recipe");
        }


        public ActionResult Info() {
            return View();
        }


        [HttpGet]
        public ActionResult Update() {
            if (HttpContext.Session.GetString("UserName") != null) {
                User model = db.Users.SingleOrDefault(e => e.UserId == (int) HttpContext.Session.GetInt32("UserId"));

                return View(model);
            }

            return RedirectToAction("List", "Recipe");
        }


        [HttpPost]
        public ActionResult Update(User model) {
            User old = db.Users.Single(e => e.UserId == model.UserId);

            // se tentar mudar email e este já estver em uso por outro usuário
            if (model.Email != old.Email & db.Users.SingleOrDefault(e => e.Email == model.Email) != null) {
                ViewBag.Exist = true;
                return View(model);
            }

            //else
            old.Name = model.Name;
            old.Email = model.Email;
            old.Password = model.Password;

            db.SaveChanges();

            return RedirectToAction("Info");
        }


        public ActionResult Delete(){
            var model = db.Users.SingleOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));

            if (model == null) {
                return RedirectToAction("List", "Recipe");
            }

            db.Users.Remove(model);
            db.SaveChanges();

            return LogOut();
        }


        public ActionResult LogOut(){
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("List", "Recipe");
        }


        public ActionResult Accept(){
            return View();
        }
}
