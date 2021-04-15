using masood_lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace masood_lab.Controllers
{
    //[masood_lab.Filters.AuthorizedUser]
    public class HomeController : Controller
    {
        Library_systemEntities1 lib = new Library_systemEntities1();
        public HomeController()
        {
            ViewBag.TotalMembers = lib.tblNewMembers_registration.Count();
            ViewBag.TotalBooks = lib.tblNewBooks_registration.Count();
            ViewBag.Registered = lib.tblBooksMembers.Count();
        }
        
        public ActionResult Index()
        {
            ViewBag.Message = Session["welcomemsg"];
            return View();
        }
       
        
    }
}