//using System.Text.Json.Serialization;

using Newtonsoft.Json;

namespace Models
{
    public class Child
    {
        public string? Name { get; set; }


        public string? DefaultString { get; set; }
        public int DefaultInt { get; set; }
        public DateTime DefaultDateTime { get; set; }
        [JsonIgnore]
        public string UnwantedString { get; set; } = "some string";

        public Parent Parent { get; set; }

        public readonly string stringField = "string";
        public bool ShouldSerializestringField()
        {
            return false;
        }
    }
    }