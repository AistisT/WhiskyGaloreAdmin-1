using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class ShippingController : Controller
    {
        public ActionResult Orders()
        {
            return View(new Shipping());
        }
    }
}