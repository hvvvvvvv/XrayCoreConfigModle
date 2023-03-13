using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.JsonConverters;

namespace XrayCoreConfigModle.Inbound
{
    [JsonConverter(typeof(InboundUnknownConfigurationConverter))]
    public class UnknownConfigurationObject:InboundConfigurationObject
    {
        private JsonObject? Content;
        public UnknownConfigurationObject(JsonObject? content = null)
        {
            Type_ = InboundServerSettingType.Unknown;
            Content = content;
        }
        public InboundConfigurationObject? ConverToSpecificType(InboundServerSettingType _type)
        {
            if(_type == InboundServerSettingType.Unknown)
            {
                return this;
            }
            return Content.Deserialize(GetInstanceType(_type)) as InboundConfigurationObject;
        }
        public void JsonWriterHandle(Utf8JsonWriter writer,JsonSerializerOptions options)
        {
            if(Content != null)
            {
                JsonSerializer.Serialize(writer, Content,options);
            }
        }
        
    }
}
