using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            if(string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.UserName))
            {
                return View("Index", model);
            }
            var validuser = false;
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {

                    var login = db.Logins.Where(x => x.Username == model.UserName && x.Passwrod == model.Password).FirstOrDefault();
                    if (login != null)
                    {
                        login.LastLoginDate = DateTime.Now;
                        db.SaveChanges();
                        validuser = true;
                        Session["login"] = login.LoginId;
                    }

                }
            }
            catch
            {
                ViewBag.Validuser = false;
            }
            if (validuser)
            {
                ViewBag.Validuser = true;              
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Validuser = false;
                ViewBag.NotValidUser = "User Does not Exists";
            }
            return View("Index", model);
        }
    }
}