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
                    /*
                    Thread thread = new Thread(() =>
                    {
                        Console.WriteLine("Sending message to server...");
                        foreach (IVIMAPP iVIMAPP in facilities.iVIMAPP)
                        {
                            //converte para binário toda a mensagem IVIM
                            Json2PerBitAdapter.Json2Bit(iVIMAPP.ivim);
                            //procura ficheiro pelo ID do primeiro IVI dentro cada IVIM
                            long countBytes = new FileInfo("JSonMessageBin"+ iVIMAPP.ivim.ivi[0].mandatory.iviIdentificationNumber + ".ivi").Length;
                            Console.WriteLine($"Número de bytes:{countBytes}");
                            foreach (Ivi item in iVIMAPP.ivim.ivi)
                            {
                                Console.WriteLine($"IVI ID:{item.mandatory.iviIdentificationNumber} ");
                                //item.mandatory.timeStamp = DateTime.UtcNow.Ticks; - solução mais detalhada mas não suportado pelas classes rootIVI por limite de bytes 
                                item.mandatory.timeStamp = DateTime.Now.TimeOfDay.Ticks;// apenas dá o tempo sem o dia
                                Console.WriteLine($"Timestamp:{new DateTime((long)item.mandatory.timeStamp)} ");
                            }
                        }
                    });
                    thread.Start();
                    */
                    Console.WriteLine("Sending message to server...");
                    foreach (IVIMAPP ivimapp in facilities.iVIMAPP)
                    {
                        Message.Send(appInterval, ivimapp.ivim, jsonObject);
                    }
                    //string jsonStringWrite = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    //File.WriteAllText("RSU1.json", jsonStringWrite);
                }
            } catch(Exception e) {
                Console.WriteLine("Ocorreu uma excepção!!");
                Console.WriteLine(e.Message);
            }
            

            
        }
    }
}



