using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneTipProject.Models.Context;
using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.Models.UnitOfWork;

namespace PhoneTipProject.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class PagesGroupsController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        public ActionResult Index()
        {
            return View(unitOfWork.pagegroup.GetAll());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagesGroup pagesGroup = unitOfWork.pagegroup.Find(id);
            if (pagesGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pagesGroup);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle")] PagesGroup pagesGroup)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.pagegroup.Add(pagesGroup);
                unitOfWork.Save();
                unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagesGroup pagesGroup = unitOfWork.pagegroup.Find(id);
            if (pagesGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pagesGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle")] PagesGroup pagesGroup)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.pagegroup.Update(pagesGroup);
                unitOfWork.Save();
                unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagesGroup pagesGroup = unitOfWork.pagegroup.Find(id);
            if (pagesGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pagesGroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagesGroup pagesGroup = unitOfWork.pagegroup.Find(id);
            unitOfWork.pagegroup.Remove(pagesGroup);
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
