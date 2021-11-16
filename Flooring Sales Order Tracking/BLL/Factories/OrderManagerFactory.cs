using BLL.Managers;
using BLL.RepoManagers;
using Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderManagerFactory
    {
        public static OrderRepoManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString().ToLower();

            switch (mode)
            {
                case "test":
                    return new OrderRepoManager(new TestOrderRepo());
                case "prod":
                    return new OrderRepoManager(new LiveOrderRepo());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
