using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class ListOrderResponse : Response
    {
        public List<Order> Orders { get; set; }
    }
}
