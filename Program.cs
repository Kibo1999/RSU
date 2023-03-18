namespace RSU // Note: actual namespace depends on the project name.
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RSU.JSonMessage;
    using System;
    using System.Diagnostics;
    using System.Threading;

    internal class Program
    {
        static void Main(string[] args)
        {
            var myJsonString = File.ReadAllText("rsu3sinais.json");
            JSonMessages myJson = JsonConvert.DeserializeObject<JSonMessages>(myJsonString);

            //getting appInterval
            int appInterval = myJson.data.ITSAPP.facilities.appinterval;
            ITSAPP app = myJson.data.ITSAPP;
            Console.WriteLine(app);
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



