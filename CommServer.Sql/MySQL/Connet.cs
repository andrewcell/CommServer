using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CommServer.Sql.MySQL
{
    public class Connet
    {
        public static string testConnection()
        {
            try
            {
                
                MySqlConnection conn = new MySqlConnection(Const.connStr);
                conn.Open();
                return "sus";
            }
            catch(MySqlException e)
            {
                return e.Message;
            }

        }
    }
}
