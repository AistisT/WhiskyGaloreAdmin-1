using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiskyGaloreAdmin.Filters;
using WhiskyGaloreAdmin.Models;
namespace WhiskyGaloreAdmin.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult RestrictedPage()
        {
            return View();
        }


        [LoggingFilter]
        // GET: /Home/Admin
        public ActionResult Admin()
        {

            return View();
        }

        // GET: /Home/Login
        public ActionResult Login()
        {
            return View(new User());
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {

                if (user.loginUser(user))
                {
                    String loginName = user.username;
                    String account = user.acctype.ToString();
                    String loggedIn = user.loggedIn.ToString();

                    //Store in session
                    Session["loginName"] = loginName;
                    Session["account"] = account;
                    Session["loggedIn"] = loggedIn;
                    Session["lg"] = user;

                    Debug.WriteLine("account type " + user.username);
                    if (account.Equals("Administrator"))
                    {
                        return RedirectToAction("Admin", "Home");
                    }
                    if (account.Equals("Manager"))
                    {
                        return RedirectToAction("Product", "Manager");
                    }
                    if (account.Equals("Shipper"))
                    {
                        return RedirectToAction("Orders", "Shipping");
                    }
                    if (account.Equals("Warehouse"))
                    {
                        return RedirectToAction("Orders", "Warehouse");
                    }
                    return View();
                }

                else
                {

                    ModelState.AddModelError("", "The username or password is incorrect.");
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }
        [LoggingFilter]
        // GET: /LogOut
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

    }
}