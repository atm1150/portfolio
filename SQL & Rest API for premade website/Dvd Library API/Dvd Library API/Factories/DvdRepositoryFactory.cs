using Dvd_Library_API.Models;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dvd_Library_API.Factories
{
    public static class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "SampleData":
                    return new DvdRepositoryMock();
                case "EntityFramework":
                    return new DvdRepositoryEF();
                default:
                    throw new Exception("Mode value in web config is not valid");
            }
        }
    }
}