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
    public class PagesGroupsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(unitOfWork.pagegroup.GetAll());
        }

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
            return View(pagesGroup);
        }

        public ActionResult Create()
        {
            return View();
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
            return View(pagesGroup);
        }

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
            return View(pagesGroup);
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
            return View(pagesGroup);
        }

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
            return View(pagesGroup);
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
    }
}
