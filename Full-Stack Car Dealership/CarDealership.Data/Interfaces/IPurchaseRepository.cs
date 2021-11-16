using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IPurchaseRepository
    {
        IEnumerable<Purchase> GetAll();
        void Insert(Purchase purchase);
        IEnumerable<SalesReportItem> SearchSales(SalesSearchParameters parameters);
    }
}