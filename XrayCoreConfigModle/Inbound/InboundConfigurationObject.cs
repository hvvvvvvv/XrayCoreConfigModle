using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class InboundConfigurationObject
    {
        protected JsonObject ItemNode;
        protected InoundSeverSettingType Type { get; set; }
        public InoundSeverSettingType GetConfigurationType() => Type;
        public InboundConfigurationObject()
        {
            ItemNode = new();
        }
    }
}
