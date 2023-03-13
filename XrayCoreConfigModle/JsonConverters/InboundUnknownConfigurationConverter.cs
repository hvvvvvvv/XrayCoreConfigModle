using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;

namespace XrayCoreConfigModle.JsonConverters
{
    public class InboundUnknownConfigurationConverter : JsonConverter<UnknownConfigurationObject>
    {
        public override UnknownConfigurationObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var Jobj = JsonSerializer.Deserialize<JsonNode>(ref reader, options);
            return new UnknownConfigurationObject(Jobj!.AsObject());
        }

        public override void Write(Utf8JsonWriter writer, UnknownConfigurationObject value, JsonSerializerOptions options)
        {
            value.JsonWriterHandle(writer, options);
        }
    }
}
