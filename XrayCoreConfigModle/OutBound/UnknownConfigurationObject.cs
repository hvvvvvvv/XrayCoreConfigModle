using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    [JsonConverter(typeof(JsonConverters.OutboundUnknownConfigurationObjectConverter))]
    public class UnknownConfigurationObject: OutboundConfigurationObject
    {
        private JsonObject? Content { get; set; }
        public UnknownConfigurationObject(JsonObject? _content = null) : base(OutboundServerSettingType.Unknown)
        {
            Content = _content;
        }
        public OutboundConfigurationObject? ConvertToSpecificType(OutboundServerSettingType type)
        {
            if(type ==GetConfigurationType())
            {
                return this;
            }
            return Content.Deserialize(GetInstanceType(type)) as OutboundConfigurationObject;
        }
        public void JsonWriteHandle(Utf8JsonWriter writer,JsonSerializerOptions options)
        {
            if(Content != null)
            {
                JsonSerializer.Serialize(writer, Content, options);
            }
        }

    }
}
