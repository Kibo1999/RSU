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
                string myJsonString = File.ReadAllText("RSU1.json");
                JSonMessages myJson = JsonConvert.DeserializeObject<JSonMessages>(myJsonString);

                Facilities myFacilities = myJson.data.ITSAPP.facilities;
                if (myFacilities != null && myFacilities.enabled)
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
                                foreach (var iVIMAPP in myJson.data.ITSAPP.facilities.iVIMAPP)
                                {
                                    foreach (var item in iVIMAPP.ivim.ivi)
                                    {
                                        Console.WriteLine($"IVI ID:{item.mandatory.iviIdentificationNumber} ");
                                        item.mandatory.timeStamp = DateTime.UtcNow.Ticks;
                                        Console.WriteLine($"Timestamp:{item.mandatory.timeStamp} ");
                                    }
                                }
                                //Console.WriteLine($"IVI ID:{myFacilities.iVIMAPP[0].ivim.ivi[0].mandatory.iviIdentificationNumber} ");
                                //Console.WriteLine($"Timestamp:{myFacilities.iVIMAPP[0].ivim.ivi[0].mandatory.timeStamp} ");
                            });
                            thread.Start();
                            Console.WriteLine($"Current number of threads: {Process.GetCurrentProcess().Threads.Count}");
                            string jsonStringWrite = JsonConvert.SerializeObject(myJson, Formatting.Indented);
                            File.WriteAllText("RSU1.json", jsonStringWrite);
                        Thread.Sleep(appInterval);
                        }
                    }
            } catch(Exception e) {
                Console.WriteLine("Ocorreu uma excepção!!");
                Console.WriteLine(e.Message);
            }
            

            
        }
    }
}



