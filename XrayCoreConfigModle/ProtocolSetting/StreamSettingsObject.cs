using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class StreamSettingsObject
    {
        /// <summary>
        /// "tcp" | "kcp" | "ws" | "http" | "domainsocket" | "quic" | "grpc"
        /// 连接的数据流所使用的传输方式类型，默认值为 "tcp"
        /// </summary>
        public string? network { get; set; }
        /// <summary>
        /// 是否启用传输层加密，支持的选项有
        /// "none" | "tls" | "xtls"| "reality"
        /// </summary>
        public string? security { get; set; }
        /// <summary>
        /// TLS 配置。TLS 由 Golang 提供，通常情况下 TLS 协商的结果为使用 TLS 1.3，不支持 DTLS。
        /// </summary>
        public TLSObject? tlsSettings { get; set; }
        /// <summary>
        /// XTLS 配置。security 为"XTLS"时生效
        /// </summary>
        public TLSObject? xtlsSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RealityObject? realitySettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TcpObject? tcpSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public KcpObject? kcpSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public WebSocketObject? wsSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpObject? httpSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public QuicObject? quicSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DomainSocketObject? dsSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GRPCObject? grpcSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SockoptObject? sockopt { get; set; }
    }
}
