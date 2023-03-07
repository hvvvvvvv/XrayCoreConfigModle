using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Dns;

namespace XrayCoreConfigModle.JsonConverters
{
    public class DnsServersConverter : JsonConverter<List<Dns.ServerObject>?>
    {
        public override List<ServerObject>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<ServerObject>? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
