using Microsoft.AspNetCore.Mvc;
using CookBook.Models;

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
}
