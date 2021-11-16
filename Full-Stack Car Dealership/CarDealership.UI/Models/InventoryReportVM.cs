using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.UI.Models
{
    public class InventoryReportVM
    {
        public IEnumerable<InventoryReportItem> NewVehicles { get; set; }
        public IEnumerable<InventoryReportItem> UsedVehicles { get; set; }
    }
}