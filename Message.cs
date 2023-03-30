using Newtonsoft.Json;
using RSU.JSonMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RSU
{
    internal class Message
    {
        public static void Send(int appInterval, Ivim ivim, JSonMessages jsonObject)
        {
            Thread thread = new Thread(() =>
            {
                while (true) {
                    //converte para binário toda a mensagem IVIM
                    Json2PerBitAdapter.Json2Bit(ivim);
                    //procura ficheiro pelo ID do primeiro IVI dentro cada IVIM
                    long countBytes = new FileInfo("JSonMessageBin" + ivim.ivi[0].mandatory.iviIdentificationNumber + ".ivi").Length;
                    Console.WriteLine($"Número de bytes:{countBytes}");
                    foreach (Ivi item in ivim.ivi)
                    {
                        Console.WriteLine($"IVI ID:{item.mandatory.iviIdentificationNumber} ");
                        //item.mandatory.timeStamp = DateTime.UtcNow.Ticks; - solução mais detalhada mas não suportado pelas classes rootIVI por limite de bytes 
                        item.mandatory.timeStamp = DateTime.Now.TimeOfDay.Ticks;// apenas dá o tempo sem o dia
                        Console.WriteLine($"Timestamp:{new DateTime((long)item.mandatory.timeStamp)} ");
                    }

                    while (true)
                    {
                        try
                        {
                            string jsonStringWrite = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                            File.WriteAllText("RSU1.json", jsonStringWrite);
                            break;
                        }
                        catch { }
                    }
                    
                    Thread.Sleep(appInterval);
                }
            });
            thread.Start();
        }
    }
}
