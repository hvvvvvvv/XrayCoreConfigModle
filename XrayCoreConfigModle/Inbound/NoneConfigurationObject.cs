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
    [JsonConverter(typeof(NoneConfigurationConverter))]
    public class NoneConfigurationObject:InboundConfigurationObject
    {
        private JsonObject? Content;
        public NoneConfigurationObject(JsonObject? content)
        {
            Type_ = InboundServerSettingType.None;
            Content = content;
        }
        public InboundConfigurationObject? ConverToSpecificType(InboundServerSettingType _type)
        {
            if(_type == InboundServerSettingType.None)
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
