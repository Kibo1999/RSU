using Newtonsoft.Json;
using RSU.JSONIvim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using PerEncDec.IVI.IVIMPDUDescriptions;

namespace RSU
{
    internal class MessageThread
    {
        private IVIMAPP? message;
        private int appInterval;
        private Json2PerBitAdapter adapter;
        private byte[] buffer;
        private DateTime unixEpoch;

        public MessageThread(IVIMAPP message, int appInterval)
        {
            this.message = message;
            this.appInterval = appInterval;
            this.adapter = new Json2PerBitAdapter();
            this.unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }


        public void Run()
        {
            

            Thread thread = new Thread(() =>
            {
                this.SetTimeStamp();
                Console.WriteLine($"Timestamp:{unixEpoch.AddMilliseconds((long)this.message.ivim.ivi.mandatory.timeStamp)}");
            

                //converte para binário toda a mensagem IVIM
                this.buffer = this.adapter.Json2Bit(this.message.ivim);
                //procura ficheiro pelo ID do primeiro IVI dentro cada IVIM
                Console.WriteLine($"Número de bytes:{this.buffer.Length}");

                while (true)
                {
                    Console.WriteLine($"IVI ID:{this.message.ivim.ivi.mandatory.iviIdentificationNumber} ");
                    Thread.Sleep(this.appInterval);
                }
            });
            thread.Start();
        }

        private void SetTimeStamp()
        {
            this.message.ivim.ivi.mandatory.timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        }
    }

    
}
