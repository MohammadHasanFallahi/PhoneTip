using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneTipProject.Controllers
{
    public class ManageEmailsController : Controller
    {
        public ActionResult ActiviationEmail()
        {
            return PartialView();
        }

        public ActionResult RecoveryPassword()
        {
            return PartialView();
        }
    }
}