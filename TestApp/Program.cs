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

            JsonObject json = JsonNode.Parse(File.ReadAllText(@"C:\Users\wanchao\Desktop\v2rayN\guiConfigs\config.json")) as JsonObject;
            // var httpserver = JsonSerializer.Deserialize<InboundServerItemObject>(json["inbounds"].AsArray()[1].ToJsonString());
            Console.WriteLine(JsonSerializer.Deserialize<InboundServerItemObject>(json["inbounds"].AsArray()[1].ToJsonString()));
        }
    }
}