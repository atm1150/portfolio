using CarDealership.Data.Factories;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Vehicle
        public ActionResult Details(int id)
        {        
            var repo = VehicleRepoFactory.GetRepository();
            var model = repo.GetDetails(id);

            return View(model);
        }

        public ActionResult New()
        {
            SearchVM model = new SearchVM();
            model.SearchYears = VehicleRepoFactory.GetRepository().CreateSearchYears().ToList();
            model.PriceRange = VehicleRepoFactory.GetRepository().CreatePriceRange().ToList();
            return View(model);
        }

        public ActionResult Used()
        {
            SearchVM model = new SearchVM();
            model.SearchYears = VehicleRepoFactory.GetRepository().CreateSearchYears().ToList();
            model.PriceRange = VehicleRepoFactory.GetRepository().CreatePriceRange().ToList();
            return View(model);
        }
    }
}