using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        private static readonly Dictionary<string, InboundServerSettingType> ServerSettingType = new()
        {
            {"socks",InboundServerSettingType.Socks },
            {"http",InboundServerSettingType.Http },
            {"dokodemo-door",InboundServerSettingType.DokodemoDoor },
            {"shadowsocks",InboundServerSettingType.Shadowsocks },
            {"trojan",InboundServerSettingType.Trojan },
            {"vless",InboundServerSettingType.Vless },
            {"vmess",InboundServerSettingType.Vmess }
        };
        public override InboundServerItemObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ret = new InboundServerItemObject();
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    var propertyInfo = ret.GetType().GetProperties().FirstOrDefault(i => i.Name == propertyName);    
                    
                    reader.Read();
                    if(propertyInfo != null)
                    {
                        var propertyValue = JsonSerializer.Deserialize(ref reader, propertyInfo.PropertyType, options);
                        propertyInfo.SetValue(ret, propertyValue);
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }
            if (ret.settings is NoneConfigurationObject noneTypeSetting)
            {
                if(ServerSettingType.TryGetValue(ret.protocol ?? "none", out InboundServerSettingType settingType))
                {
                    ret.settings = noneTypeSetting.ConverToSpecificType(settingType);
                }
            }
            return ret;
        }

        public override void Write(Utf8JsonWriter writer, InboundServerItemObject value, JsonSerializerOptions options)
        {
            if(value is null)
            {
                return;
            }
            writer.WriteStartObject();
            foreach(var propertyInfo in value.GetType().GetProperties())
            {
                var propertValue = propertyInfo.GetValue(value);
                if (propertValue != null)
                {
                    writer.WritePropertyName(propertyInfo.Name);
                    JsonSerializer.Serialize(writer, propertValue, propertyInfo.PropertyType, options);
                }
            }
            writer.WriteEndObject();
        }
    }
}
