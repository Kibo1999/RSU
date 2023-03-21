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
            try
            {
                var myJsonString = File.ReadAllText("RSU1.json");
                JSonMessages myJson = JsonConvert.DeserializeObject<JSonMessages>(myJsonString);

                Facilities myFacilities = myJson.data.ITSAPP.facilities;
                if (myFacilities != null && myFacilities.enabled)
                {
                    Thread thr = new Thread(() =>
                    {
                        //getting appInterval
                        int appInterval = myFacilities.appinterval;
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
                    });
                    thr.Start();
                }
            } catch(Exception e) {
                Console.WriteLine("Ocorreu uma excepção!!");
                Console.WriteLine(e.Message);
            }
            

            
        }
    }
}



