using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class WarehouseController : Controller
    {
        [LoggingFilter]
        public ActionResult Orders()
        {
            Warehouse w = new Warehouse();
            w.ordersTable();
            return View(w);
        }

        [LoggingFilter]
        [HttpGet]
        public ActionResult Update(int orderId)
        {
            Warehouse w = new Warehouse();
            w.orderDetails(orderId);
            return View(w);
        }

        [LoggingFilter]
        [HttpPost]
        public ActionResult Update(Warehouse w)
        {
            w.updateOrderStatus();
            ModelState.Clear();
            Warehouse a = new Warehouse();
            a.ordersTable();
            return View("Orders", a);
        }
    }
}