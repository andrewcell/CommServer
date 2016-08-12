using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Socket;
using Newtonsoft.Json;

namespace CommClient.CSharp.Library
{
    class Json
    {
        private static Random random = new Random();
        public string Login(string id, string password)
        {

        }
        public string ConnectTest()
        {
            JsonData datas = new JsonData();
            datas.data = "Connection Test";
            string JsonString = JsonConvert.SerializeObject(datas);
            return JsonString;
        }
    }
    public class JsonData
    {
        public string data { get; set; }
    }
}
