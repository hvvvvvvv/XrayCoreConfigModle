using System.Text.Json;
using XrayCoreConfigModle.Dns;
using XrayCoreConfigModle;
using System.IO;
using System.Text.Json.Nodes;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var xrayConfig = JsonHandler.JsonDeserializeFromFile<MainConfiguration>(@"C:\Users\wanchao\Desktop\v2rayN\guiConfigs\config.json");

            Console.WriteLine(JsonHandler.JsonSerializeToString(xrayConfig.inbounds));

        }
    }
}