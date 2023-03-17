namespace RSU // Note: actual namespace depends on the project name.
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RSU.JSonMessage;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

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
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine("Sending message to server...");
                    Console.WriteLine($"Current time: {DateTime.Now.TimeOfDay}");
                });
                thread.Start();
                Console.WriteLine($"Current number of threads: {Process.GetCurrentProcess().Threads.Count}");
                Thread.Sleep(appInterval);
            }
        }
    }
}



