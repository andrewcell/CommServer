using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CommServer.Sql.MySQL
{
    public class Const
    {
        public string host { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int port { get; set; }
        public string dbname { get; set; }
        public static string connStr;
        public static MySqlConnection conn;
        public static MySqlCommand cmd;
    }
}
