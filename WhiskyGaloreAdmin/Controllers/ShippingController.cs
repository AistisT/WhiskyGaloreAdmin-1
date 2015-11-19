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
    public class ShippingController : Controller
    {

        [ShipperFilter]
        public ActionResult Orders()
        {
            Shipping s = new Shipping();
            s.ordersTable();
            return View(s);
        }




        [ShipperFilter]
        [HttpGet]
        public ActionResult Update(int orderId)
        {

            if (orderId<0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (orderId<0)
            {
                return HttpNotFound();
            }

            System.Diagnostics.Debug.WriteLine("Update(int orderId)");
            Shipping s = new Shipping();
            s.orderDetails(orderId);
            return View(s);
        }

  
        [ShipperFilter]
        [HttpPost]
        public ActionResult Update(Shipping s)
        {

            if (s == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (s == null )
            {
                return HttpNotFound();
            }
            s.updateOrderStatus();
                ModelState.Clear();
                Shipping a = new Shipping();
                a.ordersTable();
                return View("Orders",a);
        }
    }
}