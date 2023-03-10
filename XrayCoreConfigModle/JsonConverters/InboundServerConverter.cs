using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private static readonly Dictionary<string, Type> ServerSettingType = new()
        {
            {"socks",typeof(SocksConfigurationObject) },
            {"http",typeof(HttpConfigurationObject) },
            {"dokodemo-door",typeof(DokodemoDoorConfigurationObject) },
            {"shadowsocks",typeof(ShadowsocksConfigurationObject) },
            {"trojan",typeof(TrojanConfigurationObject) },
            {"vless",typeof(VlessConfigurationObject) },
            {"vmess",typeof(VMessConfigurationObject) }
        };
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
            if(settingJson != null && ret.protocol != null)
            {
                if (ServerSettingType.TryGetValue(ret.protocol, out Type? settingType))
                {
                    ret.settings = (InboundConfigurationObject?)settingJson.Deserialize(settingType, options);
                }
            }
            return ret;
        }

        public override void Write(Utf8JsonWriter writer, InboundServerItemObject value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var properties in value.GetType().GetProperties())
            {
                var propertyValue = properties.GetValue(value);
                if (propertyValue != null)
                {
                    if (properties.Name == nameof(value.settings))
                    {
                        if(value.protocol != null && ServerSettingType.ContainsKey(value.protocol))
                        {
                            Type settingType = ServerSettingType[value.protocol];
                            if (settingType.IsInstanceOfType(value.settings))
                            {
                                writer.WritePropertyName(properties.Name);
                                JsonSerializer.Serialize(writer, propertyValue, settingType, options);
                            }
                        }
                    }
                    else
                    {
                        writer.WritePropertyName(properties.Name);
                        JsonSerializer.Serialize(writer, propertyValue, properties.PropertyType,options);
                    }
                }
                
            }
            writer.WriteEndObject();
        }
    }
}
