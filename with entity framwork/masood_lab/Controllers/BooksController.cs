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
    //[masood_lab.Filters.AuthorizedUser]
    public class BooksController : Controller
    {
        // GET: Books
        DBsingleton singleton = DBsingleton.getobject();

        //Books_Managers obj = new Books_Managers();
        BooksModel books = new BooksModel();

         public BooksController()
        {
            ViewBag.BooksCategory = DBsingleton.bookscategory();
        }    

        [HttpGet]
        public ActionResult Addbook()
        {
            singleton.nextid(books);        
            return View(books);
        }
        [HttpPost]
        public ActionResult Addbook(BooksModel book, HttpPostedFileBase ImageFile)
        {
            
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    ImageSaver(book, ImageFile);
                    
                    bool chk = singleton.AddBook(book);
                    if (chk)
                    {
                        TempData["Message"] = "Book Added!";
                        return RedirectToAction("ViewBook");
                    }
                    else
                    {
                        ViewBag.model = "Invalid Data! book not registere!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ImageError = "Please Upload Image";
                    return View();
                }
            }
            else
            {

                return View();
            }
           
        }

        public ActionResult ViewBook()
        {
            List<BooksModel> book = singleton.ViewBooks();
            ViewBag.Message = TempData["Message"];
            ViewBag.Error = TempData["error"];
            return View(book);
        }

        [HttpGet]
        public ActionResult UpdateBook(int FID)
        {
            BooksModel book = singleton.GetOneBook(FID);
            TempData["imagenull"] = book.ImagePath;
            return View(book);
        }
        [HttpPost]
        public ActionResult UpdateBook(BooksModel book, HttpPostedFileBase ImageFile)
        {
            book.ImagePath = TempData["imagenull"].ToString();

                if (ImageFile != null)
                {
                    ImageSaver(book, ImageFile);
                    bool check = singleton.UpdateBook(book);
                    if (check)
                    {
                        TempData["Message"] = "Book Updated!";
                        return RedirectToAction("ViewBook");
                    }
                    else
                    {
                        TempData["error"] = "Book can't be Updated!";
                        return RedirectToAction("ViewMember");
                    }

                }
                else if (book.ImagePath !=null)
                {

                    bool check = singleton.UpdateBook(book);
                    if (check)
                    {
                        TempData["Message"] = "Book Updated!";
                        return RedirectToAction("ViewBook");
                    }
                    else
                    {
                        TempData["error"] = "Book can't be Updated!";
                        return RedirectToAction("ViewMember");
                    }
                }
                else
                {
                    ViewBag.ImageError = "Please Upload Image";
                    return View();
                }

            
            
        }
        public ActionResult DeleteBook(int FID)
        {
            bool check = singleton.DeleteBook(FID);
            if (check)
            {
                TempData["Message"] = "Book Deleted!";
                return RedirectToAction("ViewBook");
            }
            else
            {
                TempData["error"] = "Book can't be Deleted!";
                return RedirectToAction("ViewBook");
            }
            
        }
        public BooksModel ImageSaver(BooksModel books, HttpPostedFileBase ImageFile)
        {
            string Filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string Extension = Path.GetExtension(ImageFile.FileName);
            Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;
            books.ImagePath = "~/ProjectData/" + Filename;
            Filename = Path.Combine(Server.MapPath("~/ProjectData/"), Filename);
            ImageFile.SaveAs(Filename);
            return books;
        }
    }
}