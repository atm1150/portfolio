using CarDealership.Data.Factories;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FeaturedVehiclesSpecialsVM model = new FeaturedVehiclesSpecialsVM();
            model.FeaturedVehicles = VehicleRepoFactory.GetRepository().GetFeaturedVehicleList();
            model.Specials = SpecialRepoFactory.GetRepository().GetAll();

            return View(model);
        }

        public ActionResult Specials()
        {
            var repo = SpecialRepoFactory.GetRepository();
            var model = repo.GetAll();

            return View(model);
        }
        [HttpGet]
        public ActionResult Contact(string vin)
        {
            var repo = ContactRepoFactory.GetRepository();
            var model = new ContactAddVM();
            if (!string.IsNullOrEmpty(vin))
            {
                model.Contact.Message = vin;
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Contact(ContactAddVM model)
        {
            if (ModelState.IsValid)
            {
                var repo = ContactRepoFactory.GetRepository();

                try
                {
                    repo.Insert(model.Contact);
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return View(model);
            }  
        }
    }
}