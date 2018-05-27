using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using _5051.Models;
using System.Web.Routing;

namespace _5051.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
        {
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include="Username,Password")] CredentialsViewModel model)
        {
            if (model.Username.ToLower() == "admin" || model.Username.ToLower() == "administrator")
            {
                return RedirectToAction("Options", "AdminPanel");
            } else if (model.Username.ToLower() == "kiosk"){
                return RedirectToAction("Index", "Kiosk");
            } else {
                return RedirectToAction("Report", new RouteValueDictionary(
                    new { controller = "RemoteStudent", action = "Report", username = model.Username }));
            }
        }
    }
}