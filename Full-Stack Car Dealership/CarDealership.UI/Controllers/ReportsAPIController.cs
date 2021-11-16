using CarDealership.Data.Factories;
using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class ReportsAPIController : ApiController
    {
        [Route("api/salesReport/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string userID, DateTime? fromDate, DateTime? toDate)
        {
            var repo = PurchaseRepoFactory.GetRepository();

            try
            {
                var parameters = new SalesSearchParameters()
                {
                    UserID = userID,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                var result = repo.SearchSales(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}