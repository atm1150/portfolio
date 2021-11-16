using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dvd_Library_API.Models
{
    public interface IDvdRepository
    {
        //return all dvds
        List<Dvd> GetAll();

        //return a specific dvd
        Dvd Get(int dvdId);

        //search repo by title and return all matches
        List<Dvd> GetByTitle(string title);

        //Search through repo by year and return all matches
        List<Dvd> GetByYear(int releaseYear);

        //search by director and return matches
        List<Dvd> GetByDirector(string director);

        //search by rating and return matches
        List<Dvd> GetByRating(string rating);

        //add a new dvd entry to repo
        void Create(Dvd newDvd);

        //edit an existing dvd
        void Update(Dvd updateDvd);

        //delte an existing dvd
        void Delete(int dvdId);
    }
}