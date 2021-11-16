using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class ListStringResponse : Response
    {
        public List<string> StringList { get; set; }
    }
}
