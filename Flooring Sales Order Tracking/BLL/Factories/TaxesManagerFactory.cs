using BLL.Managers;
using Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Factories
{
    public class TaxesManagerFactory
    {
        public static TaxRepoManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString().ToLower();

            switch (mode)
            {
                case "test":
                    return new TaxRepoManager(new TestTaxRepo());
                case "prod":
                    return new TaxRepoManager(new LiveTaxRepo());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
