using masood_lab.Models;
using masood_lab.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace masood_lab.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DBsingleton singleton = DBsingleton.getobject();


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            bool chk = singleton.LoginFunc(login);
            if (chk)
            {
                if (login.identification == "member")
                {
                    Session["role"] = login.identification;
                    Session["image"] = login.ImagePath;
                    Session["name"] = login.Name + " " + login.id;
                    Session["id"] = login.id;
                    Session["welcomemsg"] = "Welcome " + login.Name + " " + login.id;
                    return RedirectToAction("Index", "Home");
                }
                else if (login.identification == "admin")
                {
                    Session["role"] = login.identification;
                    Session["image"] = login.ImagePath;
                    Session["name"] = login.Name + " " + login.id;
                    Session["welcomemsg"] = "Welcome " + login.Name + " " + login.id;
                    return RedirectToAction("Index", "Home");
                }
                else if (login.identification == "librarian")
                {
                    Session["role"] = login.identification;
                    Session["image"] = login.ImagePath;
                    Session["name"] = login.Name + " " + login.id;
                    Session["welcomemsg"] = "Welcome " + login.Name + " " + login.id;
                    return RedirectToAction("Index", "Home");

                }

            }
            else
            {
                ViewBag.Message = "Invalid LoginID or Password";
            }
            return View();

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}