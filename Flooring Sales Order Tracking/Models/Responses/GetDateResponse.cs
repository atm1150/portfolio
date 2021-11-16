using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class GetDateResponse : Response
    {
        public DateTime OrderDate { get; set; }
    }
}
