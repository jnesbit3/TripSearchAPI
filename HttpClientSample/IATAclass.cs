//Used to deserialize the JSON recieved from the IATA API
//Alternatively, the same JSON is stored in a static file
//IATAcode.json in the debug folder.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class IataResponse
    {        
        public List<Response> response = new List<Response>();
    }
    public class Response
    {
        public string code { get; set; }
        public string name { get; set; }
    }
}
