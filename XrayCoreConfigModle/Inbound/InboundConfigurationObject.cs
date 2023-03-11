using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    [JsonConverter(typeof(JsonConverters.ConfigurationConverter))]
    public abstract class InboundConfigurationObject
    {
        protected  InboundServerSettingType Type_ { get; set; }
        public InboundServerSettingType GetConfigurationType() => Type_;
        public InboundConfigurationObject()
        {
        }
        public static Type GetInstanceType(InboundServerSettingType type)
        {
            Type ret;
            switch (type)
            {
                case InboundServerSettingType.Http:
                    ret = typeof(HttpConfigurationObject);
                    break;
                case InboundServerSettingType.Socks:
                    ret = typeof(SocksConfigurationObject);
                    break;
                case InboundServerSettingType.Shadowsocks:
                    ret = typeof(ShadowsocksConfigurationObject);
                    break;
                case InboundServerSettingType.Trojan:
                    ret = typeof(TrojanConfigurationObject);
                    break;
                case InboundServerSettingType.Vmess:
                    ret = typeof(VMessConfigurationObject);
                    break;
                case InboundServerSettingType.Vless:
                    ret = typeof(VlessConfigurationObject);
                    break;
                case InboundServerSettingType.DokodemoDoor:
                    ret = typeof(DokodemoDoorConfigurationObject);
                    break;
                case InboundServerSettingType.None:
                    ret = typeof(NoneConfigurationObject);
                    break;
                default:
                    throw new ArgumentException(nameof(type));
            }
            return ret;
        }
        public static Type GetInstanceType(InboundConfigurationObject obj)
        {
            return GetInstanceType(obj.GetConfigurationType());
        }
    }
}
