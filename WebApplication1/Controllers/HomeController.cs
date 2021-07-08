using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (Session["login"] == null)
            {
                return Redirect("~/Login");
            }
            try
            {
                var fileName = "";
                var filePath = "";
                if (file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    filePath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    file.SaveAs(filePath);
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Users.Add(new User
                        {
                            Filename = fileName,
                            FilePath = filePath,
                            UploadTime = DateTime.Now,
                            LoginId = Convert.ToInt32(Session["login"])
                        });

                        db.SaveChanges();
                        ViewBag.Message = "File Uploaded Successfully!!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "File upload failed!!";
                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
            }
            return View();
        }
        
    }
}