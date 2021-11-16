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
    public class ProductRepoManager
    {
        private IProductRepository _repo;

        public ProductRepoManager(IProductRepository repo)
        {
            _repo = repo;
        }

        public ProductListResponse LoadProducts()
        {
            ProductListResponse response = new ProductListResponse();

            List<Product> productList = _repo.LoadProducts();

            if (productList.Count > 0)
            {
                response.Success = true;
                response.ProductList = productList;
                return response;
            }
            else
            {
                response.Success = false;
                response.ProductList = null;
                response.Message = "No products were returned. Please check the associated storage file";
                return response;
            }

            throw new NotImplementedException();
        }

        public OrderResponse AddProductDataToOrderObject(string productName)
        {
            OrderResponse response = _repo.AddProductDataToOrderObject(productName);

            return response;
        }
    }
}
