using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IProductRepository
    {
        List<Product> LoadProducts();

        OrderResponse AddProductDataToOrderObject(string productName);
    }
}
