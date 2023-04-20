namespace RSU // Note: actual namespace depends on the project name.
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RSU.JSONIvim;
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
                JSonMessage jsonObject = JsonConvert.DeserializeObject<JSonMessage>(jsonInString);
                Facilities facilities = jsonObject.data.ITSAPP.facilities;
                if (facilities != null && facilities.enabled)
                {
                    //getting appInterval
                    int appInterval = facilities.appinterval;
                    Console.WriteLine($"AppInterval : {appInterval} ");

                    foreach (IVIMAPP ivimapp in facilities.iVIMAPP)
                    {
                        if (ivimapp.enabled) {

                            MessageThread message = new MessageThread(ivimapp,appInterval);
                            message.Run();
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



