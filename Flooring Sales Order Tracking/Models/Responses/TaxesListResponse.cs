using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class TaxesListResponse : Response
    {
        public List<Taxes> TaxesList { get; set; }
    }
}
