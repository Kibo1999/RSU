namespace RSU // Note: actual namespace depends on the project name.
{
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

                    foreach (IVIMAPP ivimapp in facilities.iVIMAPP)
                    {
                        if (ivimapp.enabled) {
                            Message message = new Message { jsonObject = jsonObject };
                            message.Send(appInterval, ivimapp.ivim);
                        }
                        
                    }
                }
            } catch(Exception e) {
                Console.WriteLine("Ocorreu uma excepção!!");
                Console.WriteLine(e.Message);
            }
            

            
        }
    }
}



