using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.OutBound;

namespace XrayCoreConfigModle.JsonConverters
{
    public class OutboundUnknownConfigurationObjectConverter : JsonConverter<UnknownConfigurationObject>
    {
        public override UnknownConfigurationObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = JsonSerializer.Deserialize<JsonObject>(ref reader, options);
            return new(value);
        }

        public override void Write(Utf8JsonWriter writer, UnknownConfigurationObject value, JsonSerializerOptions options)
        {
            if(value != null)
            {
                value.JsonWriteHandle(writer, options);
            }
        }
    }
}
