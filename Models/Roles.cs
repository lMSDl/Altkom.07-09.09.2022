using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Roles
    {
        Read = 1 << 0,
        Update = 1 << 1,
        Delete = 1 << 2,
        Create = 1 << 3,

        Admin = 1 << 31
    }



}
