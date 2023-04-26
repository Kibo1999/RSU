namespace RSU // Note: actual namespace depends on the project name.
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RSU.JSONIvim;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Net.Http.Headers;

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Insira ID do rsu: ");
                String answer = Console.ReadLine();
                int id = Int32.Parse(answer);

                string jsonInString = "";
                using (HttpClient client = new HttpClient())
                {

                    var syncTask = new Task<String>(() =>
                    {
                        return client.GetStringAsync($"http://localhost:5233/configfile?id={id}").Result;
                    });
                    syncTask.RunSynchronously();
                    //Console.WriteLine(syncTask.Result);
                    jsonInString = syncTask.Result;
                }

                JSonMessage jsonObject = JsonConvert.DeserializeObject<JSonMessage>(jsonInString);
                Facilities facilities = jsonObject.data.ITSAPP.facilities;
                if (facilities != null && facilities.enabled)
                {
                    //getting appInterval
                    int appInterval = facilities.appinterval;
                    Console.WriteLine($"AppInterval : {appInterval} ");

                    foreach (IVIMAPP ivimapp in facilities.iVIMAPP)
                    {
                        if (ivimapp.enabled)
                        {
                            MessageThread message = new MessageThread(ivimapp, appInterval);
                            message.Run();
                        }

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu uma excepção!!");
                Console.WriteLine(e.Message);
            }



        }
    }
}



