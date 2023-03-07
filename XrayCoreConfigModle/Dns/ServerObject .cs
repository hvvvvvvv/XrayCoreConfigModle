using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Dns
{
    /// <summary>
    /// DNS服务器定义，支持的形式较多
    /// 详情参阅 https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/dns.md#serverobject
    /// </summary>
    public class ServerObject
    {
        /// <summary>
        ///
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string>? domains { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string>? expectIPs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool skipFallback { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? clientIP { get; set; }

        public static implicit operator ServerObject(string value)
        {
            bool isSuccess = false;
            var AddrAndPort = value.Split(':');
            string addr = string.Empty;
            int port = default;
            if(AddrAndPort.Length == 2)
            {
                string domainNamePattern = @"[a-zA-Z][a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*";
                string ipAddrPattern = @"(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
                if (Regex.IsMatch(AddrAndPort[0], $"^(({domainNamePattern})|({ipAddrPattern}))$"))
                {
                    if (int.TryParse(AddrAndPort[1], out port))
                    {
                        if(port >= 1 && port <= 0xffff)
                        {
                            addr = AddrAndPort[0];
                            isSuccess = true;
                        }
                    }
                }
            }
            if(!isSuccess)
            {
                throw new ArgumentException("参数字符串格式不正确",nameof(value));
            }
            return new()
            {
                address = addr,
                port = port
            };

        }
    }
}
