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
    public class RolesController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(unitOfWork.Roles.GetAll());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = unitOfWork.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,RoleTitel,RoleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Roles.Add(roles);
                unitOfWork.Save();
                unitOfWork.Dispose();
                return RedirectToAction("Index");
            }

            return View(roles);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = unitOfWork.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return PartialView(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleID,RoleTitel,RoleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Roles.Update(roles);
                unitOfWork.Save();
                unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            return View(roles);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = unitOfWork.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return PartialView(roles);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roles roles = unitOfWork.Roles.Find(id);
            unitOfWork.Roles.Remove(roles);
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
