using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;

namespace WhiskyGaloreAdmin.Controllers
{
    public class ProductController : Controller
    {

        [AdminFilter]
        // GET: /Product/Details
        public ActionResult Details()
        {
            return View(new Product());
        }

        [AdminFilter]
        // GET: /Product/Edit
        public ActionResult Edit(int productId)
        {
            Debug.WriteLine("in controller " + productId);
            if (productId < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (productId < 0)
            {
                Debug.WriteLine("not found");
                return HttpNotFound();
            }
            Product p = new Product(productId);
            return View(p);
        }

        [AdminFilter]
        // POST: /Product/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p, string command)
        {
            if (p == null || string.IsNullOrEmpty(command))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (p == null || string.IsNullOrEmpty(command))
            {
                return HttpNotFound();
            }

            if (command.Equals("Update"))
            {
                Debug.WriteLine("c " + command);
                if (ModelState.IsValid)
                {
                    p.updateProduct(p);
                }
                else
                {
                    return View(p);
                }
            }
            else
            {
                Debug.WriteLine("c " + command);
                p.deleteProduct(p.productId);
            }
            return RedirectToAction("Details");

        }

        [AdminFilter]
        // GET: /Product/Add
        public ActionResult Add()
        {
            
            return View(new Product());
        }

        [AdminFilter]
        // POST: /Product/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product p)
        {
            if (p == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (p == null )
            {
                return HttpNotFound();
            }

            Debug.WriteLine("ins " + p.SelectedCat);
            if (ModelState.IsValid)
            {
                Debug.WriteLine("id = ");
                p.insertProduct(p);
            }
            else
            {
                Debug.WriteLine("id != ");
                return View(p);
            }
            return RedirectToAction("Details");
        }
	}
}