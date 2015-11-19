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
            if (System.Web.HttpContext.Current.Session["account"] != null)
            {
                if (System.Web.HttpContext.Current.Session["account"].ToString() == "Administrator")
                {
                    var adminData = new AdminNavBar();
                    return PartialView("_AdminNav", adminData.navbarItems().ToList());
                }

                else if (System.Web.HttpContext.Current.Session["account"].ToString() == "Manager")
                {
                    var managerData = new ManagerNavBar();
                    return PartialView("_ManagerNav", managerData.navbarItems().ToList());
                }

                else if (System.Web.HttpContext.Current.Session["account"].ToString() == "Shipper")
                {
                    var ShipperData = new ShipperNavBar();
                    return PartialView("_ShipperNav", ShipperData.navbarItems().ToList());
                }

                else if (System.Web.HttpContext.Current.Session["account"].ToString() == "Warehouse")
                {
                    var WarehouseData = new ShipperNavBar();
                    return PartialView("_WarehouseNav", WarehouseData.navbarItems().ToList());
                }

                return RedirectToRoute("Default"); ;
            }

            else
            {
                System.Diagnostics.Debug.WriteLine("retudirecting to route");
                return RedirectToRoute("Default", new { controller = "Home", action = "Login", id = UrlParameter.Optional });
            }
        }
    }
}