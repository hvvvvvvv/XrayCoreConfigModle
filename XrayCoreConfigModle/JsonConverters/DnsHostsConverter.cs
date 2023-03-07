using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.JsonConverters
{
    public class DnsHostsConverter : JsonConverter<Dictionary<string, List<string>>?>
    {
        public override Dictionary<string, List<string>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, List<string>>? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
