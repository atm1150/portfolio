using CarDealership.Data.Factories;
using CarDealership.UI.Models;
using CarDealership.UI.Utitlites;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarDealership.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            InventoryReportVM model = new InventoryReportVM();
            model.NewVehicles = VehicleRepoFactory.GetRepository().GetInventoryReport("new").ToList();
            model.UsedVehicles = VehicleRepoFactory.GetRepository().GetInventoryReport("used").ToList();

            return View(model);
        }


        public ActionResult Sales()
        {
            SalesSearchVM model = new SalesSearchVM();
            var userList = AuthorizeUtilities.GetUsersInRole("sales");
            model.Users = from u in userList
                          select new SelectListItem { Text = u.FirstName + ' ' + u.LastName, Value = u.Id.ToString() };

            return View(model);
        }
    }
}