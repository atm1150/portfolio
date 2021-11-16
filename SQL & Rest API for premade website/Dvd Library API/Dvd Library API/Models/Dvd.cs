using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dvd_Library_API.Models
{
    public class Dvd
    {
        public int id { get; set; }
        public string title { get; set; }
        public int releaseYear { get; set; }
        public string director { get; set; }

        //rating should be an enum but already set up the database and don't feel like changing the sql
        public string rating { get; set; }
        public string notes { get; set; }

    }
}