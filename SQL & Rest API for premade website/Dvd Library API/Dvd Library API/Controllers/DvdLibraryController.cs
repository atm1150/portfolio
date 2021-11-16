using Dvd_Library_API.Factories;
using Dvd_Library_API.Models;
using System.Web.Http;
using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dvd_Library_API.Controllers
{
    //turn on cors for all
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdLibraryController : ApiController
    {
        IDvdRepository dvdRepository = DvdRepositoryFactory.GetRepository();

        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(dvdRepository.GetAll());
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int id)
        {
            Dvd dvd = dvdRepository.Get(id);

            if(dvd == null)
            {
                return NotFound();
            }

            return Ok(dvd);
        }

        [Route("dvd/")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(Dvd dvd)
        {
            dvdRepository.Create(dvd);

            return Created($"dvd/{dvd.id}", dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void Update( int id, Dvd dvd)
        {
            dvdRepository.Update(dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            dvdRepository.Delete(id);
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetbyTitle(string title)
        {
            List<Dvd> found = dvdRepository.GetByTitle(title);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvds/director/{director}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetbyDirector(string director)
        {
            List<Dvd> found = dvdRepository.GetByDirector(director);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetbyRating(string rating)
        {
            List<Dvd> found = dvdRepository.GetByRating(rating);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetbyYear(int releaseYear)
        {
            List<Dvd> found = dvdRepository.GetByYear(releaseYear);

            if (found == null)
                return NotFound();

            return Ok(found);
        }
    }
}