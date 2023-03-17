using System;

namespace RSU // Note: actual namespace depends on the project name.
{
    using Newtonsoft.Json;

    internal class Program
    {
        static void Main(string[] args)
        {
            var myJsonString = File.ReadAllText("rsu3sinais.json");
            var myJson = JsonConvert.DeserializeObject(myJsonString);

            //Console.WriteLine(myJsonString);
            Console.WriteLine(myJson);
        }
    }
}



