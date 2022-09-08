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
    //[DataContract]
    public class User : Entity
    {
        //[DataMember]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        //[DataMember]
        public string SetPassword { set { Password = value; } }
        [JsonIgnore]
        //[DataMember]
        public string Password { get; set; }


        /*public bool ShouldSerializePassword()
        {
            return false;
        }*/
    }
}
