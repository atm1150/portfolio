using Dvd_Library_API.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dvd_Library_API.Models
{
    public class DvdRepositoryEF : IDvdRepository
    {
        //calling the EF initialize DB context and models
        static DvdLibraryEntities repository = new DvdLibraryEntities();

        IEnumerable<Dvd> dvds = repository.Dvds;

        public void Create(Dvd newDvd)
        {
            if (dvds.Any())
            {
                newDvd.id = dvds.Max(d => d.id) + 1;
            }
            else
            {
                newDvd.id = 0;
            }

            repository.Dvds.Add(newDvd);
            repository.SaveChanges();
        }

        public void Delete(int id)
        {
            Dvd dvd = dvds.FirstOrDefault(d => d.id == id);
            if (dvd != null)
            {
                repository.Dvds.Remove(dvd);
                repository.SaveChanges();
            }
        }

        public Dvd Get(int id)
        {
            return dvds.FirstOrDefault(d => d.id == id);
        }

        public List<Dvd> GetAll()
        {
            return dvds.ToList();
        }

        public List<Dvd> GetByDirector(string director)
        {
            List<Dvd> dvdList = dvds.Where(d => d.director.ToUpper().Contains(director.ToUpper())).ToList();
            return dvdList;
        }

        public List<Dvd> GetByRating(string rating)
        {
            return dvds.Where(d => d.rating.ToUpper() == rating.ToUpper()).ToList();
        }

        public List<Dvd> GetByTitle(string title)
        {
            List<Dvd> dvdList = dvds.Where(d => d.title.ToUpper().Contains(title.ToUpper())).ToList();
            return dvdList;
        }

        public List<Dvd> GetByYear(int releaseYear)
        {
            return dvds.Where(d => d.releaseYear == releaseYear).ToList();
        }

        public void Update(Dvd updateDvd)
        {
            Dvd dvd = dvds.FirstOrDefault(d => d.id == updateDvd.id);
            repository.Entry(dvd).CurrentValues.SetValues(updateDvd);
            repository.SaveChanges();
        }
    }
}