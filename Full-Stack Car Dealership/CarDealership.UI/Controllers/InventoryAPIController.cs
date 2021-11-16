using CarDealership.Data.Factories;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class InventoryAPIController : ApiController
    {
        [Route("api/inventory/search")]
        [AcceptVerbs("Get")]
        public IHttpActionResult Search(decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, string searchTextBox, string vehicleType)
        {
            var repo = VehicleRepoFactory.GetRepository();

            try
            {
                var parameters = new InventorySearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    SearchTextBox = searchTextBox,
                    VehicleType = vehicleType
                };

                var result = repo.Search(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}