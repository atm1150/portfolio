using CarDealership.Data.ADO;
using CarDealership.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Factories
{
    public class BodyStyleRepoFactory
    {
        public static IBodyStyleRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new BodyStyleRepositoryADO();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
