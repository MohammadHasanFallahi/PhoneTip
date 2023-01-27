using PhoneTipProject.Models.DataLayer;
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
                if (!unitOfWork.Users.GetAll().Any(x => x.Email == Register.Email.Trim().ToLower()))
                {
                    Users user = new Users()
                    {
                        UserName = Register.UserName.Trim(),
                        Email = Register.Email.Trim().ToLower(),
                        IsActive = false,
                        PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(Register.PassWord, "MD5"),
                        ActiveCode = Guid.NewGuid().ToString(),
                        RegisterDate = DateTime.Now,
                        RoleID = 1
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

        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login([Bind(Include = "Email,PassWord,RememberMe")] LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.PassWord, "MD5");
                var user = unitOfWork.Users.GetAll().SingleOrDefault(x => x.Email == login.Email && x.PassWord == hashpassword);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده یافت نشد!");
                }
            }
            return View("Login");
        }

        [Route("LogOff")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult ActiveUser(string id)
        {
            var user = unitOfWork.Users.GetAll().SingleOrDefault(x => x.ActiveCode == id);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            unitOfWork.Save();
            unitOfWork.Dispose();
            ViewBag.username = user.UserName;
            return View();
        }

        [Route("ForgotPassword")]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (ModelState.IsValid)
            {
                Users user = unitOfWork.Users.GetAll().SingleOrDefault(x => x.Email == forgot.Email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        string body = PartialToStringClass.RenderPartialView("ManageEmails", "RecoveryPassword", user);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", body);
                        return View("SuccessForgotPassword", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با این ایمیل یافت نشد!");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordRecovery(string id, RecoveryPassword recovery)
        {
            if (ModelState.IsValid)
            {
                Users user = unitOfWork.Users.GetAll().SingleOrDefault(x => x.ActiveCode == id);
                if (user != null)
                {
                    user.PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.PassWord, "MD5");
                    user.ActiveCode = Guid.NewGuid().ToString();
                    unitOfWork.Users.Update(user);
                    unitOfWork.Save();
                    unitOfWork.Dispose();
                    return Redirect("/Login?Recovery=true");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            return View();
        }

        [Route("ShowUserDetails")]
        [HttpGet]
        public ActionResult ShowUserDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                Users user = unitOfWork.Users.GetAll().SingleOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null)
                return View(user);
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

        [Route("ShowUserDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowUserDetails(Users user)
        {
            if (user != null && user.UserName.Trim()!= "")
            {
                Users edituser = unitOfWork.Users.GetAll().SingleOrDefault(x => x.UserID == user.UserID);
                if (edituser != null)
                {
                    edituser.UserName = user.UserName;
                    unitOfWork.Users.Update(edituser);
                    unitOfWork.Save();
                    unitOfWork.Dispose();
                    ViewBag.successedit = "ذخیره اطلاعات با موفقیت انجام شد";
                    return View(edituser);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            else
            {
                return Redirect("/");
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