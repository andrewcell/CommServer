using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CommServer
{
    class Json
    {
        JsonTextReader reader;
        string ReadSettings (string data)
        {
            try
            {
                string jsondata = File.ReadAllText("conf/settings.conf");
                reader = new JsonTextReader(new StringReader(jsondata));
                return "";
            }
            catch (FileNotFoundException e)
            {
                return e.Message;
            }
            

        }
        static bool NewSQL(string host, string password, int port, string name, string prefix)
        {
            return false;
        }
    }
}
