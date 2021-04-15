using masood_lab.Models;
using masood_lab.Patterns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace masood_lab.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        AdminModel Librarian = new AdminModel();
        DBsingleton singleton = DBsingleton.getobject();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            singleton.getnextid(Librarian);
            return View(Librarian);
        }
        [HttpPost]
        public ActionResult AddAdmin(AdminModel Admin, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {

                if (ImageFile != null)
                {
                    ImageSaver(Librarian, ImageFile);
                    Admin.ImagePath = TempData["imgfile"].ToString();
                    bool chk = singleton.AddAdmin(Admin);
                    if (chk)
                    {
                        ViewBag.added = "Admin Registered!";
                        return View();
                        //return RedirectToAction("Addmember", "Member");
                    }
                    else
                    {
                        ViewBag.error = "username already registered!";
                        return View();
                    }

                }
                else
                {
                    ViewBag.ImageError = "Plz Upload Image";
                    return View();
                }


            }
            else
            {

                return View();
            }


        }

        public AdminModel ImageSaver(AdminModel admin, HttpPostedFileBase ImageFile)
        {
            string Filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string Extension = Path.GetExtension(ImageFile.FileName);
            Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;
            admin.ImagePath = "~/ProjectData/" + Filename;

            Filename = Path.Combine(Server.MapPath("~/ProjectData/"), Filename);
            ImageFile.SaveAs(Filename);
            TempData["imgfile"] = admin.ImagePath;
            return admin;
        }

    }
}