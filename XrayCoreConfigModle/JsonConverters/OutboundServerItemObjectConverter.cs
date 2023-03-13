using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.OutBound;

namespace XrayCoreConfigModle.JsonConverters
{
    public class OutboundServerItemObjectConverter : JsonConverter<OutboundServerItemObject>
    {
        public override OutboundServerItemObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            
            var retValue = new OutboundServerItemObject();
            var properties = retValue.GetType().GetProperties();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if(reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }
                var propertyName = reader.GetString();
                var propertyInfo = properties.FirstOrDefault(p => p.Name == propertyName);
                if (propertyInfo != null)
                {
                    reader.Read();
                    var properyValue = JsonSerializer.Deserialize(ref reader, propertyInfo.PropertyType, options);
                    propertyInfo.SetValue(retValue, properyValue);
                }
            }
            if(retValue.settings is UnknownConfigurationObject unknownConfiguration)
            {
                var settingTpe = retValue.protocol switch
                {
                    "http" => OutboundServerSettingType.Http,
                    "socks" => OutboundServerSettingType.Socks,
                    "shadowsocks" => OutboundServerSettingType.Shadowsocks,
                    "trojan" => OutboundServerSettingType.Trojan,
                    "vmess" => OutboundServerSettingType.Vmess,
                    "vless" => OutboundServerSettingType.Vless,
                    "dns" => OutboundServerSettingType.Dns,
                    "wireguard" => OutboundServerSettingType.WireGuard,
                    "freedom" => OutboundServerSettingType.Freedom,
                    "blackhole" => OutboundServerSettingType.Blackhole,
                    _ => OutboundServerSettingType.Unknown,
                };
                retValue.settings = unknownConfiguration.ConvertToSpecificType(settingTpe);
            }
            return retValue;
        }

        public override void Write(Utf8JsonWriter writer, OutboundServerItemObject value, JsonSerializerOptions options)
        {
            if(value != null)
            {
                writer.WriteStartObject();
                foreach(var propertyInfo in value.GetType().GetProperties())
                {
                    var writeValue = propertyInfo.GetValue(value);
                    if(writeValue != null)
                    {
                        writer.WritePropertyName(propertyInfo.Name);
                        JsonSerializer.Serialize(writer,writeValue, propertyInfo.PropertyType,options);
                    }
                }
                writer.WriteEndObject();
            }
        }
    }
}
