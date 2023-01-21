﻿using PhoneTipProject.Models.DataLayer;
using PhoneTipProject.Models.UnitOfWork;
using PhoneTipProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhoneTipProject.Utilities.ExtensionMethods;
using System.Net;

namespace PhoneTipProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route("Register")]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
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
                    string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActiviationEmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعالسازی", body);
                    return View("SuccessRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری می باشد");
                }
            } 
            return View("RegisterUser");
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ActiveUser(string id)
        {
            var user = unitOfWork.Users.GetAll().SingleOrDefault(x => x.ActiveCode == id);
            if(user==null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            unitOfWork.Save();
            unitOfWork.Dispose();
            ViewBag.username = user.UserName;
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