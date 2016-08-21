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
        public static bool testConfigure()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM accounts", Const.conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    
                }
                return true;
               
            }
            catch (MySqlException ef)
            {
                
                Console.WriteLine(ef.Message);
                return false;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return false;
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
