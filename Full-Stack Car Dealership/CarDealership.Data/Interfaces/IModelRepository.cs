using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IModelRepository
    {
        IEnumerable<Model> GetAll();
        void Insert(Model model);
        IEnumerable<ModelListItem> GetModelList();
        IEnumerable<Model> GetModelsByMake(int id);
    }
}