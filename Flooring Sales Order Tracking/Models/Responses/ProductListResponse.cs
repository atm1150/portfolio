using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class ProductListResponse : Response
    {
        public List<Product> ProductList { get; set; }
    }
}
