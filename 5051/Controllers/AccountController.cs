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
    /// <summary>
    /// The Account controller processes the login credentials for the website.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        // NOTE: For The Endians's official POST method, please see the AddStudent POST in the
        // Admin controller and the associated AddStudent view.
        //
        /// <summary>
        /// Handle log-in requests for the website.
        /// </summary>
        /// <param name="model"></param>  // A view model containing username and password credentials
        /// <returns>Redirect to proper admin/kiosk/student page.</returns>
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include="Username,Password")] CredentialsViewModel model)
        {
            // High-level description: check if user is an admin or kiosk, else default to a student.
            if (model.Username.ToLower() == "admin" || model.Username.ToLower() == "administrator")
            {
                return RedirectToAction("Index", "Admin");
            } else if (model.Username.ToLower() == "kiosk")
            {
                return RedirectToAction("Index", "Kiosk");
            } else
            {
                return RedirectToAction("Report", new RouteValueDictionary(
                    new { controller = "RemoteStudent", action = "Report", username = model.Username }));
            }
        }
    }
}