using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using CommServer.Sql.MySQL;

namespace CommServer
{
    class Json
    {
        JsonTextReader reader;
        public string ReadMySQLSettings (string data)
        {
            try
            {
                string jsondata = File.ReadAllText("../../conf/mysql.json");
                Const con = JsonConvert.DeserializeObject<Const>(jsondata);
                Const.connStr = "server=" + con.host + ";uid=" + con.username + ";pwd=" + con.password + ";database=" + con.dbname;

                return "Success";
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
