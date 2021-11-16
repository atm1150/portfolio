using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dvd_Library_API.Models
{
    public class DvdRepositoryMock : IDvdRepository
    {
        //mock repo list. static as only one exists in the entirety of the application
        private static List<Dvd> _dvds = new List<Dvd>();

        private Dvd dvd1 = new Dvd
        {
            id = 1,
            title = "The Lion King",
            releaseYear = 1994,
            director = "Roger Allers",
            rating = "G",
            notes = "Lion with family issues"
        };

        private Dvd dvd2 = new Dvd
        {
            id = 2,
            title = "Titanic",
            releaseYear = 1997,
            director = "James Cameron",
            rating = "PG-13",
            notes = "Love story about a sinking ship"
        };

        private Dvd dvd3 = new Dvd
        {
            id = 3,
            title = "Batman",
            releaseYear = 1989,
            director = "Tim Burton",
            rating = "PG-13",
            notes = "Rich billionaire attempts alternative therapy for coping with loss"
        };

        private Dvd dvd4 = new Dvd
        {
            id = 4,
            title = "Star Wars",
            releaseYear = 1977,
            director = "George Lucas",
            rating = "PG",
            notes = "Laser swords and an evil cyborg in space"
        };

        //this variable determines if the initial list has been added before
        private static bool _hasBeenInitialized = false;

        

        public void Create(Dvd newDvd)
        {
            HasInitializeOccured();

            if (_dvds.Any())
            {
                newDvd.id = _dvds.Max(d => d.id) + 1;
            }
            else
            {
                newDvd.id = 0;
            }

            _dvds.Add(newDvd);
            
        }

        public void Delete(int id)
        {
            HasInitializeOccured();
            _dvds.RemoveAll(d => d.id == id);
        }

        public Dvd Get(int id)
        {
            HasInitializeOccured();
            return _dvds.First(d => d.id == id);
        }

        public List<Dvd> GetAll()
        {
            HasInitializeOccured();
            return _dvds;
        }

        public List<Dvd> GetByDirector(string director)
        {
            HasInitializeOccured();

            return _dvds.FindAll(d => d.director.ToLower() == director.ToLower());
        }

        //should use an enum for ratings but don't feel like doing it currently
        public List<Dvd> GetByRating(string rating)
        {
            HasInitializeOccured();
            return _dvds.FindAll(d => d.rating.ToUpper() == rating.ToUpper());
        }

        public List<Dvd> GetByTitle(string title)
        {
            HasInitializeOccured();
            return _dvds.FindAll(d => d.title.ToUpper() == title.ToUpper());
        }

        public List<Dvd> GetByYear(int releaseYear)
        {
            HasInitializeOccured();
            return _dvds.FindAll(d => d.releaseYear == releaseYear);
        }

        public void Update(Dvd updateDvd)
        {
            HasInitializeOccured();
            _dvds.RemoveAll(d => d.id == updateDvd.id);
            _dvds.Add(updateDvd);
        }

        private void AddEntriesToList()
        {
            _dvds.Add(dvd1);
            _dvds.Add(dvd2);
            _dvds.Add(dvd3);
            _dvds.Add(dvd4);
        }

        private void HasInitializeOccured()
        {
            if (_hasBeenInitialized == false)
            {
                _hasBeenInitialized = true;
                AddEntriesToList();
            }
        }

    }
}