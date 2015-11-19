using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class ShippingController : Controller
    {
        [LoggingFilter]
        public ActionResult Orders()
        {
            Shipping s = new Shipping();
            s.ordersTable();
            return View(s);
        }

        [LoggingFilter]
        [HttpGet]
        public ActionResult Update(int orderId)
        {
            Shipping s = new Shipping();
            s.orderDetails(orderId);
            return View(s);
        }

        [LoggingFilter]
        [HttpPost]
        public ActionResult Update(Shipping s)
        {
            s.updateOrderStatus();
            ModelState.Clear();
            Shipping a = new Shipping();
            a.ordersTable();
            return View("Orders", a);
        }
    }
}