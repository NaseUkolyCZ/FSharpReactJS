using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharePointAppWeb.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            User spUser = null;

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            var RedirectTo = ConfigurationManager.AppSettings["RedirectTo"];
            if (string.IsNullOrWhiteSpace(RedirectTo))
            {
                return View();
            }
            else
            {
                return Redirect(RedirectTo + "?accessToken=" + spContext.UserAccessTokenForSPHost);
            }
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
    }
}
