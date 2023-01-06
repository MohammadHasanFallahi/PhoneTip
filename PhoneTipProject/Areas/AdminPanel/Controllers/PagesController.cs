using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneTipProject.Models.Context;
using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.Models.UnitOfWork;

namespace PhoneTipProject.Areas.AdminPanel.Controllers
{
    public class PagesController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        public ActionResult Index()
        {
            var pages = unitOfWork.Pages.GetAll();
            return View(pages);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = unitOfWork.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(unitOfWork.pagegroup.GetAll(), "GroupID", "GroupTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,Titel,ShortDescription,Text,Visit,ImageUrl,ShowInSlider,CreateDate")] Pages pages,HttpPostedFileBase imgurl)
        {
            if (ModelState.IsValid)
            {
                if (imgurl != null)
                {
                    string fileurl = "/Upload/Pages/" + Guid.NewGuid().ToString().Substring(0, 8) + imgurl.FileName;
                    imgurl.SaveAs(Server.MapPath("~" + fileurl));
                    pages.ImageUrl = fileurl;
                }
                pages.Visit = 0;
                pages.CreateDate = DateTime.Now;
                unitOfWork.Pages.Add(pages);
                unitOfWork.Save();
                unitOfWork.Dispose();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(unitOfWork.pagegroup.GetAll(), "GroupID", "GroupTitle", pages.GroupID);
            return View(pages);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = unitOfWork.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(unitOfWork.pagegroup.GetAll(), "GroupID", "GroupTitle", pages.GroupID);
            return View(pages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,Titel,ShortDescription,Text,Visit,ImageUrl,ShowInSlider,CreateDate")] Pages pages,HttpPostedFileBase imgurl)
        {
            if (ModelState.IsValid)
            {
                if (imgurl != null)
                {
                    FileInfo fileInfo = new FileInfo(Server.MapPath("~"+pages.ImageUrl));
                    if (fileInfo.Exists)
                    { 
                        fileInfo.Delete();
                    }
                    string fileurl = "/Upload/Pages/" + Guid.NewGuid().ToString().Substring(0, 8) + imgurl.FileName;
                    imgurl.SaveAs(Server.MapPath("~" + fileurl));
                    pages.ImageUrl = fileurl;
                }

                pages.CreateDate = DateTime.Now;
                unitOfWork.Pages.Update(pages);
                unitOfWork.Save();
                unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(unitOfWork.pagegroup.GetAll(), "GroupID", "GroupTitle", pages.GroupID);
            return View(pages);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = unitOfWork.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return PartialView(pages);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pages pages = unitOfWork.Pages.Find(id);
            FileInfo fileInfo = new FileInfo(Server.MapPath("~" + pages.ImageUrl));
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            unitOfWork.Pages.Remove(pages);
            unitOfWork.Save();
            unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
