using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Dns
{
    public class ServerString: DnsServer
    {
        string Value { get; set; }
        public ServerString(string value): base(DnsServerInputMode.String)
        {
            Value = value;
        }
        public void JsonWriteHandle(Utf8JsonWriter writer,JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, Value,options);
        }
        public static implicit operator ServerString(string value)
        {
            return new ServerString(value);
        }
    }
}
