using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models
{
    public class Credentials
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
