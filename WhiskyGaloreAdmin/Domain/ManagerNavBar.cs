using WhiskyGaloreAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiskyGaloreAdmin.Domain
{
    public class ManagerNavBar
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 2, nameOption = "Products", controller = "Manager", action = "Whisky", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 3, nameOption = "Orders", controller = "Manager", action = "Orders", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 4, nameOption = "Staff Details", controller = "Manager", action = "Staff", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 5, nameOption = "Add Daily Hours", controller = "DailyHours", action = "Add", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 6, nameOption = "Edit Daily Hours", controller = "DailyHours", action = "Details", imageClass = "fa fa-edit fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 7, nameOption = "Daily Finances", controller = "Finances", action = "DailyFinances", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 8, nameOption = "Monthly Finances", controller = "Finances", action = "MonthlyFinances", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 9, nameOption = "Yearly Finances", controller = "Finances", action = "YearlyFinances", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            return menu.ToList();
        }
    }
}