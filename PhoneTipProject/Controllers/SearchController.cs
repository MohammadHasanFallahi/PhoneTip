using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.Models.UnitOfWork;
using PhoneTipProject.ViewModels;

namespace PhoneTipProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        public ActionResult Index(string q)
        {
            ViewBag.Search = q;
            var search = unitOfWork.Pages.GetAll().Where(x => x.Text.Contains(q) || x.Titel.Contains(q)  || x.ShortDescription.Contains(q)).Distinct();
            return View(search);
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