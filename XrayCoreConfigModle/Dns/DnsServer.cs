using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Dns
{
    public abstract class DnsServer
    {
        private DnsServerInputMode Mode;
        public DnsServerInputMode GetMode() => Mode;
        public DnsServer(DnsServerInputMode mode)
        {
            Mode = mode;
        }
        public Type GetInstanceType(DnsServer server)
        {
            return server.GetMode() switch
            {
                DnsServerInputMode.Object => typeof(ServerObject),
                DnsServerInputMode.String => typeof(ServerString),
                _ => throw new ArgumentException()
            };
        }
    }
}
