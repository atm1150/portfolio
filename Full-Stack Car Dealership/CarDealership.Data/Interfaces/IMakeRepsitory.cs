using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IMakeRepository
    {
        IEnumerable<Make> GetAll();
        void Insert(Make make);
        IEnumerable<MakeListItem> GetMakeList();
    }
}