using CarDealership.Data.Factories;
using CarDealership.UI.Models;
using CarDealership.UI.Utitlites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    [Authorize(Roles = "sales")]
    public class SalesController : Controller
    {
        // GET: Sales    
        public ActionResult Index()
        {
            SearchVM model = new SearchVM();
            model.SearchYears = VehicleRepoFactory.GetRepository().CreateSearchYears().ToList();
            model.PriceRange = VehicleRepoFactory.GetRepository().CreatePriceRange().ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Purchase(int id)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserID = AuthorizeUtilities.GetUserId(this);
            }

            var repo = VehicleRepoFactory.GetRepository();
            var staterepo = StateRepoFactory.GetRepository();
            var model = new AddPurchaseVM();
            model.VehicleDetails = repo.GetDetails(id);
            model.Purchase.VehicleID = id;
            model.SetStateItems(staterepo.GetAll());
            return View(model);
        }

        [HttpPost]
        public ActionResult Purchase(AddPurchaseVM model)
        {
            if (Request.IsAuthenticated)
            {
                model.Purchase.UserID = AuthorizeUtilities.GetUserId(this);
            }

            if (ModelState.IsValid)
            {
                var customerRepo = CustomerRepoFactory.GetRepository();
                var purchaseRepo = PurchaseRepoFactory.GetRepository();
                try
                {
                    customerRepo.Insert(model.Customer);

                    model.Purchase.CustomerID = model.Customer.CustomerID;
                    model.Purchase.VehicleID = model.VehicleDetails.VehicleID;

                    purchaseRepo.Insert(model.Purchase);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var repo = VehicleRepoFactory.GetRepository();
                var staterepo = StateRepoFactory.GetRepository();
                
                model.VehicleDetails = repo.GetDetails(model.VehicleDetails.VehicleID);
                model.Purchase.VehicleID = model.VehicleDetails.VehicleID;
                model.SetStateItems(staterepo.GetAll());

                return View(model);
            }
        }

    }
}