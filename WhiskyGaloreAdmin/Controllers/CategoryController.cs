﻿using System;
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
    public class CategoryController : Controller
    {
        [AdminFilter]
        // GET: /Category/Details
        public ActionResult Details()
        {
            return View(new Category());
        }
        [AdminFilter]
        // GET: /Category/Edit
        public ActionResult Edit(int categoryId)
        {
            Debug.WriteLine("in controller " + categoryId);
            if (categoryId < 0)
            {
                //Debug.WriteLine("null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (categoryId < 0)
            {
                Debug.WriteLine("not found");
                return HttpNotFound();
            }
            Category c = new Category(categoryId);
            return View(c);
        }
        [AdminFilter]
        // POST: /Category/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category c)
        {

            if (c==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (c == null)
            {
                return HttpNotFound();
            }
           
            if (ModelState.IsValid)
            {
                Debug.WriteLine("id = " + c.catId);
                c.updateCategory(c);
            }
            else
            {
                Debug.WriteLine("id != ");
                return View(c);
            }
            return RedirectToAction("Details");
        }
        [AdminFilter]
        // GET: /Category/Add/
        public ActionResult Add()
        {

            return View(new Category());
        }
        [AdminFilter]
        // POST: /Category/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category c)
        {

            if (c == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (c == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                Debug.WriteLine("id = ");
                c.insertCategory(c);
            }
            else
            {
                Debug.WriteLine("id != ");
                return View(c);
            }
            return RedirectToAction("Details");
        }
    }
}