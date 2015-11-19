using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class WarehouseController : Controller
    {
        [WarehouseFilter]
        public ActionResult Orders()
        {
            Warehouse w = new Warehouse();
            w.ordersTable();
            return View(w);
        }

        [WarehouseFilter]
        [HttpGet]
        public ActionResult Update(int orderId)
        {
            if (orderId < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (orderId < 0)
            {
                return HttpNotFound();
            }


            Warehouse w = new Warehouse();
            w.orderDetails(orderId);
            return View(w);
        }

        [WarehouseFilter]
        [HttpPost]
        public ActionResult Update(Warehouse w)
        {
            if (w == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (w == null)
            {
                return HttpNotFound();
            }
            w.updateOrderStatus();
            ModelState.Clear();
            Warehouse a = new Warehouse();
            a.ordersTable();
            return View("Orders", a);
        }
    }
}