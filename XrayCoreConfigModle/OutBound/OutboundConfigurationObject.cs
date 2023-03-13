using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;
using XrayCoreConfigModle.JsonConverters;

namespace XrayCoreConfigModle.OutBound
{
    [JsonConverter(typeof(OutboundConfigurationObjectConverter))]
    public abstract class OutboundConfigurationObject
    {
        private OutboundServerSettingType _type;
        public OutboundServerSettingType GetConfigurationType() => _type;
        public OutboundConfigurationObject(OutboundServerSettingType type = OutboundServerSettingType.Unknown)
        {
            _type = type;
        }
        public static Type GetInstanceType(OutboundServerSettingType type)
        {
            return type switch
            {
                OutboundServerSettingType.Http => typeof(HttpConfigurationObject),
                OutboundServerSettingType.Socks => typeof(SocksConfigurationObject),
                OutboundServerSettingType.Shadowsocks => typeof(ShadowsocksConfigurationObject),
                OutboundServerSettingType.Trojan => typeof(TrojanConfigurationObject),
                OutboundServerSettingType.Vmess => typeof(VMessConfigurationObject),
                OutboundServerSettingType.Vless => typeof(VlessConfigurationObject),
                OutboundServerSettingType.Dns => typeof(DnsConfiguration),
                OutboundServerSettingType.Blackhole => typeof(BlackholeConfigurationObject),
                OutboundServerSettingType.Freedom => typeof(FreedomConfigurationObject),
                OutboundServerSettingType.WireGuard => typeof(WireguardConfigurationObject),
                OutboundServerSettingType.Unknown => typeof(UnknownConfigurationObject),
                _ => throw new InvalidOperationException()
            };
        }
        public static Type GetInstanceType(OutboundConfigurationObject obj)
        {
            return GetInstanceType(obj.GetConfigurationType());
        }
    }
}
