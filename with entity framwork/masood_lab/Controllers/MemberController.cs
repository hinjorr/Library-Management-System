using masood_lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using masood_lab.Patterns;

namespace masood_lab.Controllers
{
    //[masood_lab.Filters.AuthorizedUser]
    public class MemberController : Controller
    {

        //GET: Student
        DBsingleton singleton = DBsingleton.getobject();
        MemberModel member = new MemberModel();

        [HttpGet]
        public ActionResult AddMember()
        {
           MemberModel members= singleton.getnextid();
            return View(members);
        }
    
        [HttpPost]
        public ActionResult AddMember(MemberModel member, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {

                    if (ImageFile != null)
                    {
                       ImageSaver(member,ImageFile);
                       bool chk = singleton.AddMember(member);
                       
                       if (chk)
                       {
                           TempData["Message"] = "Member Registered!";
                           return RedirectToAction("ViewMember");
                       }
                       else
                       {
                           ViewBag.duplication = "Username already exist!";
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

        public ActionResult ViewMember()
        {
            IList<MemberModel> members = singleton.Viewmember();
            ViewBag.Message = TempData["Message"];
            ViewBag.Error = TempData["error"];
            return View(members);
        }


        [HttpGet]
        public ActionResult UpdateMember(int FID)
        {
            MemberModel member = singleton.GetOneMember(FID);
            TempData["imagenull"] = member.ImagePath;
            return View(member);
        }
       [HttpPost]
        public ActionResult UpdateMember(MemberModel member, HttpPostedFileBase ImageFile)
        {
            member.ImagePath = TempData["imagenull"].ToString();
            
                if (ImageFile != null )
                {
                    ImageSaver(member, ImageFile);
                    bool check = singleton.UpdateMember(member);
                    if (check)
                    {
                        TempData["Message"] = "Member Updated!";
                        return RedirectToAction("ViewMember");
                    }
                    else
                    {
                        TempData["error"] = "Member can't be Updated!";
                        return RedirectToAction("ViewMember");
                    }

                }

                else if (member.ImagePath != null)
                {
                    bool check = singleton.UpdateMember(member);
                    if (check)
                    {
                        TempData["Message"] = "Member Updated!";
                        return RedirectToAction("ViewMember");
                    }
                    else
                    {
                        TempData["error"] = "Member can't be Updated!";
                        return RedirectToAction("ViewMember");
                    }
                }

                else
                {
                    ViewBag.ImageError = "Please Upload Image";
                    return View();
                }

            
      
        }

       public ActionResult DeleteMember(int FID)
       {
           bool check = singleton.DeleteMember(FID);
           if (check)
           {
               TempData["Message"] = "Member Deleted!";
               return RedirectToAction("ViewMember");
           }
           else
           {
               TempData["error"] = "Member can't be Deleted!";
               return RedirectToAction("ViewMember");
           }

          
       }

       public MemberModel ImageSaver(MemberModel member, HttpPostedFileBase ImageFile)
       {
           string Filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
           string Extension = Path.GetExtension(ImageFile.FileName);
           Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;
           member.ImagePath = "~/ProjectData/" + Filename;

           Filename = Path.Combine(Server.MapPath("~/ProjectData/"), Filename);
           ImageFile.SaveAs(Filename);
           return member;
       }

       
    }
}