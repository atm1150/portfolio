using CarDealership.Data.Factories;
using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.UI.Controllers
{
    public class SalesAPIController : ApiController
    {
        [Route("api/sales/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, string searchTextBox)
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
                    SearchTextBox = searchTextBox
                };

                var result = repo.AbleToBeSold(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
