namespace RSU // Note: actual namespace depends on the project name.
{
    using EncoderIvi.Message;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RSU.JSonMessage;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string jsonInString = File.ReadAllText("RSU1.json");
                JSonMessages jsonObject = JsonConvert.DeserializeObject<JSonMessages>(jsonInString);

                Facilities facilities = jsonObject.data.ITSAPP.facilities;
                if (facilities != null && facilities.enabled)
                {
                    
                        //getting appInterval
                        int appInterval = facilities.appinterval;
                        Console.WriteLine($"AppInterval : {appInterval} ");
                    
                        while (true)
                        {
                            Thread thread = new Thread(() =>
                            {
                                Console.WriteLine("Sending message to server...");
                                foreach (IVIMAPP iVIMAPP in facilities.iVIMAPP)
                                {
                                    foreach (Ivi item in iVIMAPP.ivim.ivi)
                                    {
                                        Console.WriteLine($"IVI ID:{item.mandatory.iviIdentificationNumber} ");
                                        item.mandatory.timeStamp = DateTime.UtcNow.Ticks;
                                        Console.WriteLine($"Timestamp:{new DateTime((long)item.mandatory.timeStamp)} ");
                                    }
                                }
                                //Console.WriteLine($"IVI ID:{myFacilities.iVIMAPP[0].ivim.ivi[0].mandatory.iviIdentificationNumber} ");
                                //Console.WriteLine($"Timestamp:{myFacilities.iVIMAPP[0].ivim.ivi[0].mandatory.timeStamp} ");
                            });
                            thread.Start();
                            Console.WriteLine($"Current number of threads: {Process.GetCurrentProcess().Threads.Count}");
                            string jsonStringWrite = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
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



