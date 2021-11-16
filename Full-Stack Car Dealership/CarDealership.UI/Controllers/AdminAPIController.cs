using CarDealership.Data.Factories;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.UI.Controllers
{
    public class AdminAPIController : ApiController
    {
        [Route("api/make/add/{makeName}/{userID}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddMake(string makeName, string userID)
        {
            var repo = MakeRepoFactory.GetRepository();
            var model = new Make();
            model.MakeName = makeName;
            model.UserID = userID;

            try
            {
                repo.Insert(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/model/add/{modelName}/{makeID}/{userID}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddModel(string modelName, int makeID, string userID)
        {
            var repo = ModelRepoFactory.GetRepository();
            var model = new Model();
            model.ModelName = modelName;
            model.UserID = userID;
            model.MakeID = makeID;

            try
            {
                repo.Insert(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/models/list")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModels(int id)
        {
            var repo = ModelRepoFactory.GetRepository();

            try
            {
                var result = repo.GetModelsByMake(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}