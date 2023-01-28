using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneTipProject.Models.UnitOfWork;
using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.ViewModels;
using PhoneTipProject.Utilities.ExtensionMethods;

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

        public ActionResult UsersControl()
        {
            List<PageCommentsViewModel> list_pagecomments = unitOfWork.PageComments.GetAll().Select(x => new PageCommentsViewModel()
            {
                CommentID=x.CommentID,
                PageTitel=x.Pages.Titel,
                FullName=x.FullName,
                Email=x.Email,
                Comment=x.Comment,
                IsActive=x.IsActive,
                CreateDate=x.CreateDate
            }).ToList();
            return View(list_pagecomments);
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}