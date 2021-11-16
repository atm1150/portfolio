using Models;
using Models.Interfaces;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class TaxRepoManager
    {
        private ITaxesRepository repo;
        OrderResponse orderResponse = new OrderResponse();

        public TaxRepoManager(ITaxesRepository _repo)
        {
            repo = _repo;
        }
        public TaxesListResponse LoadTaxes()
        {
            TaxesListResponse response = new TaxesListResponse();

            List<Taxes> taxList = repo.LoadTaxes();

            if (taxList.Count > 0)
            {
                response.Success = true;
                response.TaxesList = taxList;
                return response;
            }
            else
            {
                response.Success = false;
                response.TaxesList = null;
                response.Message = "No state taxes were returned. Please check the associated storage file";
                return response;
            }

            throw new NotImplementedException();
        }

        public OrderResponse AddTaxDataToOrderObject(string stateName)
        {
            orderResponse = repo.AddTaxDataToOrderObject(stateName);
            return orderResponse;
        }
    }
}
