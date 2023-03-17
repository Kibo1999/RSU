using System;

namespace RSU // Note: actual namespace depends on the project name.
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            var myJsonString = File.ReadAllText("rsu3sinais.json");
            JObject myJson = (JObject) JsonConvert.DeserializeObject(myJsonString);

            //getting appInterval
            int appInterval = (int) myJson["data"]["ITSAPP"]["Facilities"]["AppInterval"];

            Console.WriteLine($"AppInterval : {appInterval} ");

            while (true)
            {
                Console.WriteLine("Sending message to server...");
                Console.WriteLine($"Current time: {DateTime.Now.TimeOfDay}");
                System.Threading.Thread.Sleep( appInterval );
            }
        }
    }
}



