using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;

namespace XrayCoreConfigModle.JsonConverters
{
    public class InboundServerConverter : JsonConverter<InboundServerItemObject>
    {
        public override InboundServerItemObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonNode? settingJson = default;
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var ret = new InboundServerItemObject();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }
                string propertyName = reader.GetString()!;
                
                var property = typeToConvert.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

                if (property != null && reader.Read())
                {
                    Type propertyType = property.PropertyType;
                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        if (propertyName == "settings")
                        {
                            settingJson = JsonSerializer.Deserialize<JsonNode>(ref reader, options);
                        }
                        else
                        {
                            object? propertyValue = JsonSerializer.Deserialize(ref reader, propertyType, options);
                            property.SetValue(ret, propertyValue);
                        }
                    }
                }
            }
            if(settingJson != null)
            {
            //    switch(ret.protocol)
            //    {
            //        case "socks":
            //            ret.settings = settingJson.Deserialize<SocksConfigurationObject>(options);
            //            break;
            //        case "http":
            //            ret.settings = settingJson.Deserialize<HttpConfigurationObject>(options);
            //            break;
            //        case "dokodemo-door":
            //            ret.settings = settingJson.Deserialize<DokodemoDoorConfigurationObject>(options);
            //            break;
            //    }    
            }
            return ret;
        }

        public override void Write(Utf8JsonWriter writer, InboundServerItemObject value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
