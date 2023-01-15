using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneTipProject.Models.UnitOfWork;
using PhoneTipProject.Models.DataLayer;

namespace PhoneTipProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult ShowSlider()
        {
            IEnumerable<Pages> Pages_Slider = unitOfWork.Pages.GetAll().Where(x => x.ShowInSlider == true);
            return PartialView(Pages_Slider);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}