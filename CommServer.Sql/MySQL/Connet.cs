using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace CommServer.Sql.MySQL
{
    public class Connet
    {

        public static string testConnection()
        {
            try
            {
                Const.conn = new MySqlConnection(Const.connStr);
                Const.conn.Open();
                return "sus";
            }
            catch(MySqlException e)
            {
                return e.Message;
            }

        }
        public static string detectVersion()
        {
            return Const.conn.ServerVersion;
        }
        public static short testAccount()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM accounts", Const.conn);
               // MySqlDataReader reader = cmd.ExecuteReader();
                var auto = cmd.ExecuteScalar();
                return Convert.ToInt16(auto);

               
            }
            catch (MySqlException ef)
            {
                
                Console.WriteLine(ef.Message);
                return -1;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
    }
    public class SQLWorks
    {
        public static bool Setup()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(File.ReadAllText("../../../CommServer.Sql/MySQL/Setup.sql"));
                cmd.Connection = Const.conn;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
            
        }
    }
}
