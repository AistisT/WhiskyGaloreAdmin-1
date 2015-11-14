using WhiskyGaloreAdmin.Domain;
using WhiskyGaloreAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhiskyGaloreAdmin.Controllers
{
    public class NavbarController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            //Debug.WriteLine("this is in" + Session["loginName"].ToString());
            //Debug.WriteLine("this is in" + Session["account"].ToString());
            //var model = (User) Session["lg"] ?? new User();
            var model = new User();
            //model.
            if (System.Web.HttpContext.Current.Session["account"].ToString() == "Administrator")
            {
                var adminData = new AdminNavBar();
                return PartialView("_AdminNav", adminData.navbarItems().ToList());
            }

            var data = new Data();
            return PartialView("_Navbar", data.navbarItems().ToList());
        }
    }
}