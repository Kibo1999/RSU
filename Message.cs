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
        public JSonMessages jsonObject { get; set; }


        public void Send(int appInterval, List<IVIMAPP> iVIMAPPs)
        {
            while (true)
            {
                Thread thread = new Thread(() =>
                {
            
                Console.WriteLine("Sending message to server...");
                foreach (IVIMAPP iVIMAPP in iVIMAPPs)
                {

                    //converte para binário toda a mensagem IVIM
                    Json2PerBitAdapter.Json2Bit(iVIMAPP.ivim);
                    //procura ficheiro pelo ID do primeiro IVI dentro cada IVIM
                    long countBytes = new FileInfo("JSonMessageBin" + iVIMAPP.ivim.ivi[0].mandatory.iviIdentificationNumber + ".ivi").Length;
                    Console.WriteLine($"Número de bytes:{countBytes}");
                    foreach (Ivi item in iVIMAPP.ivim.ivi)
                    {
                        Console.WriteLine($"IVI ID:{item.mandatory.iviIdentificationNumber} ");
                        //item.mandatory.timeStamp = DateTime.UtcNow.Ticks; - solução mais detalhada mas não suportado pelas classes rootIVI por limite de bytes 
                        item.mandatory.timeStamp = DateTime.Now.TimeOfDay.Ticks;// apenas dá o tempo sem o dia
                        Console.WriteLine($"Timestamp:{new DateTime((long)item.mandatory.timeStamp)} ");
                    }

                    //cria uma thread só para escrever no ficheiro para manter o tempo entre cada mensagem
                    Thread threadToWrite = new Thread(() => WriteToFile());
                    threadToWrite.Start();

                }

                });
       
            thread.Start();
            Thread.Sleep(appInterval);
            }
        }

        private void WriteToFile()
        {
            while (true)
            {
                try
                {
                    string jsonInString = File.ReadAllText("RSU1.json");
                    jsonObject = JsonConvert.DeserializeObject<JSonMessages>(jsonInString);
                    string jsonStringWrite = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    File.WriteAllText("RSU1.json", jsonStringWrite);
                    break;
                }
                catch { }
            }
        }
    }
}
