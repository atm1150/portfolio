using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle GetByID(int vehicleID);
        IEnumerable<Vehicle> GetAll();
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
        VehicleDetailsItem GetDetails(int vehicleID);
        IEnumerable<FeaturedVehicleItem> GetFeaturedVehicleList();
        IEnumerable<VehicleListItem> Search(InventorySearchParameters parameters);
        IEnumerable<VehicleListItem> AbleToBeSold(InventorySearchParameters parameters);
        IEnumerable<InventoryReportItem> GetInventoryReport(string vehicleType);
        IEnumerable<int> CreateSearchYears();
        IEnumerable<int> CreatePriceRange();
        void Delete(int vehicleID);
    }
}