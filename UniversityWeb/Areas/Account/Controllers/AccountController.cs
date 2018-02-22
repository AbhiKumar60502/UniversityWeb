using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityWeb.Areas.Account.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Account/

        public ActionResult StudentAccount()
        {
            ViewBag.Message = "Create Student Account";
            return View("~/Areas/Account/Views/CreateAccount.cshtml");
        }

        [HttpPost]
        public ActionResult CreateAccount()
        {
            ViewBag.Message = "Student Account Created";

            //if everything is OK then return Account Created View
            //return RedirectToAction("CreateAccountSuccess", "Account",new { area = "Account" }); //if you do this then the URL will be messed up
            return RedirectToAction("CreateAccountSuccess");
            //return new RedirectResult(Url.Action("StudentAccount") + "#/AccountCreated");
        }

        public ActionResult CreateAccountSuccess()
        {
            return View("~/Areas/Account/Views/AccountCreated.cshtml");
            //return Redirect("#/CreateAccountSuccess");
        }

    }
}
