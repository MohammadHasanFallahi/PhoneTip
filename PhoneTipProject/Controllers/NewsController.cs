using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.Models.UnitOfWork;
using PhoneTipProject.ViewModels;

namespace PhoneTipProject.Controllers
{
    public class NewsController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowGroups()
        {
            var list_pagegroups = unitOfWork.pagegroup.GetAll().Select(x => new ShowGroupsViewModel() { GroupID = x.GroupID, GroupTitle = x.GroupTitle, PagesCount = x.Pages.Count }).ToList();
            return PartialView(list_pagegroups);
        }

        [HttpGet]
        public ActionResult ShowGroupsInMenu()
        {
            IEnumerable<PagesGroup> pagesGroup = unitOfWork.pagegroup.GetAll();
            return PartialView(pagesGroup);
        }

        [HttpGet]
        public ActionResult ShowTopNews()
        {
            var topnews = unitOfWork.Pages.GetAll().OrderByDescending(x => x.Visit).Take(4);
            return PartialView(topnews);
        }

        public ActionResult ShowLastNews()
        {
            IEnumerable<Pages> list_pages = unitOfWork.Pages.GetAll().OrderByDescending(x => x.CreateDate).Take(6);
            return PartialView(list_pages);
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}