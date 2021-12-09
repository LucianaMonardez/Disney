using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Response
{
    public class ResponseApi
    {

        public bool Ok { get; set; }
        public string Error { get; set; }
        public object Return { get; set; }
    }
}
