using WhiskyGaloreAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiskyGaloreAdmin.Domain
{
    public class AdminNavBar
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 2, nameOption = "Shipper Accounts", controller = "Shipper", action = "Details", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 3, nameOption = "Create New Shipper", controller = "Shipper", action = "Register", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 4, nameOption = "Employee Accounts", controller = "Employee", action = "Details", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 5, nameOption = "Create New Employee", controller = "Employee", action = "Register", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 6, nameOption = "Products", controller = "Product", action = "Details", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 7, nameOption = "Create New Product", controller = "Product", action = "Add", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 8, nameOption = "Categories", controller = "Category", action = "Details", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 9, nameOption = "Create New Category", controller = "Category", action = "Add", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 0 });
            return menu.ToList();
        }
    }
}