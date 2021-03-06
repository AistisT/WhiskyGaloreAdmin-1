﻿using WhiskyGaloreAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiskyGaloreAdmin.Domain
{
    public class WarehouseNavBar
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, nameOption = "Orders", controller = "Warehouse", action = "Orders", imageClass = "fa fa-table fa-fw", status = true, isParent = false, parentId = 0 });
            return menu.ToList();
        }
    }
}