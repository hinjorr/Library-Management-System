using masood_lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace masood_lab.Controllers
{
    public class AbstractBooksController : Controller
    {
        // GET: AbstractBooks
        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult AddBook(BooksModel model)
        {

            return Json(true);

        }
    }
}