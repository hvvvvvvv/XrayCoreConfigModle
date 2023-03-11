using System.Text.Json;
using XrayCoreConfigModle.Dns;
using XrayCoreConfigModle;
using System.IO;
using System.Text.Json.Nodes;
using XrayCoreConfigModle.Inbound;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            JsonObject json = JsonNode.Parse(File.ReadAllText(@"C:\Users\wan\Desktop\v2rayN\guiConfigs\config.json")) as JsonObject;
            var httpserver = JsonSerializer.Deserialize<List<InboundServerItemObject>>(json["inbounds"].ToJsonString());

            var JsonOption = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            Console.WriteLine(JsonSerializer.Serialize(httpserver, JsonOption));

        }
    }
}