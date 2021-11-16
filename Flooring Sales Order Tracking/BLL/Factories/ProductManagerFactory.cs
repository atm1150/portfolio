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
    public class ProductManagerFactory
    {
        public static ProductRepoManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString().ToLower();

            switch (mode)
            {
                case "test":
                    return new ProductRepoManager(new TestProductRepo());
                case "prod":
                    return new ProductRepoManager(new LiveProductRepo());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
