using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dvd_Library_API.Models.EF
{
    public class DvdLibraryEntities : DbContext
    {
        public DvdLibraryEntities()
            : base("DvdLibrary")
        {

        }
        public DbSet<Dvd> Dvds { get; set; }
    }
}