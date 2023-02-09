using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.Models.UnitOfWork;
using PhoneTipProject.ViewModels;
using WebMarkupMin.AspNet4.Mvc;

namespace PhoneTipProject.Controllers
{
    public class NewsController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ShowGroups()
        {
            var list_pagegroups = unitOfWork.pagegroup.GetAll().Select(x => new ShowGroupsViewModel() { GroupID = x.GroupID, GroupTitle = x.GroupTitle, PagesCount = x.Pages.Count }).ToList();
            return PartialView(list_pagegroups);
        }

        public ActionResult ShowGroupsInMenu()
        {
            IEnumerable<PagesGroup> pagesGroup = unitOfWork.pagegroup.GetAll();
            return PartialView(pagesGroup);
        }


        public ActionResult ShowTopNews()
        {
            var topnews = unitOfWork.Pages.GetAll().OrderByDescending(x => x.Visit).Take(4);
            return PartialView(topnews);
        }

        [HttpGet]
        public ActionResult ShowLastNews()
        {
            IEnumerable<Pages> list_pages = unitOfWork.Pages.GetAll().OrderByDescending(x => x.CreateDate).Take(6);
            return PartialView(list_pages);
        }

        [HttpGet]
        [Route("Group/{GroupID}/{Titel}")]
        public ActionResult ShowNewsInArshive(int GroupID, string Titel)
        {
            IEnumerable<Pages> pages = unitOfWork.Pages.GetAll().Where(x => x.GroupID == GroupID);
            ViewBag.Titel = Titel;
            return View(pages);
        }

        [MinifyHtml]
        [HttpGet]
        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            else
            {
                Pages pages = unitOfWork.Pages.Find(id);
                if (pages == null)
                    return HttpNotFound();
                else
                {
                    pages.Visit += 1;
                    unitOfWork.Pages.Update(pages);
                    unitOfWork.Save();
                    return View(pages);
                }
            }
        }

        [HttpGet]
        public ActionResult AddComments(int id, string FullName, string Comment)
        {
            if (FullName.Trim() != "" && Comment.Trim() != "")
            {
                if (User.Identity.IsAuthenticated)
                {
                    Users user = unitOfWork.Users.GetAll().Single(x => x.UserName == User.Identity.Name);
                    if (user != null)
                    {
                        PageComments pageComments = new PageComments()
                        {
                            PageID = id,
                            FullName = FullName,
                            Email = user.Email,
                            Comment = Comment,
                            CreateDate = DateTime.Now,
                            IsActive = true
                        };
                        unitOfWork.PageComments.Add(pageComments);
                        unitOfWork.Save();
                        var page_message = unitOfWork.PageComments.GetAll().Where(x => x.PageID == id);
                        unitOfWork.Dispose();
                        return PartialView("ShowComments", page_message);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        public ActionResult ShowComments(int id)
        {
            var page_message = unitOfWork.PageComments.GetAll().Where(x => x.PageID == id);
            return PartialView(page_message);
        }

        
        public ActionResult NewsManagement()
        {
            if (unitOfWork.Users.GetAll().Any(x => x.UserName == User.Identity.Name && x.RoleID == 2))
            { 
                ViewBag.Active = true;
                return PartialView();            
            }
            else
            {
                ViewBag.Active = false;
                return PartialView();
            }
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