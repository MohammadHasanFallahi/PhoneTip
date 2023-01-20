using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.Models.UnitOfWork;
using PhoneTipProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhoneTipProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser([Bind(Include = "UserName,Email,PassWord,RePassWord")] RegistersViewModel Register)
        {
            if (ModelState.IsValid)
            {
                if (!unitOfWork.Users.GetAll().Any(x=>x.Email==Register.Email.Trim().ToLower()))
                {
                    Users user = new Users(){ 
                    UserName=Register.UserName.Trim(),
                    Email=Register.Email.Trim().ToLower(),
                    IsActive=false,
                    PassWord=FormsAuthentication.HashPasswordForStoringInConfigFile(Register.PassWord,"MD5"),
                    ActiveCode=Guid.NewGuid().ToString(),
                    RegisterDate=DateTime.Now,
                    RoleID=1
                    };

                    unitOfWork.Users.Add(user);
                    unitOfWork.Save();
                    unitOfWork.Dispose();
                    return View("SuccessRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Emali", "ایمیل وارد شده تکراری می باشد");
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
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