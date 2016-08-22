using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;
using System.IO;
using CommServer.Sql;
using CommServer.Sql.MySQL;



namespace CommServer
{
    class Program
    {
        private static string data = "true";   
        
        static void Main(string[] args)
        {
            Json json = new Json();
            Connet conn = new Connet();
            if (CheckSettingsExists() == false)
            {
                Console.WriteLine("Error - We can't recognize setting file. Please read documentation. Program exiting..");
                Console.ReadLine();
                Environment.Exit(1);
            }
            
            json.ReadMySQLSettings("");
            if(Connet.testConnection() != "sus")
            {
                Console.WriteLine(Connet.testConnection());
                Console.ReadLine();
                Environment.Exit(1);
            }
            Console.WriteLine("MySQL Database Connected.");
            Console.WriteLine("MySQL Server : " + Connet.detectVersion());
            short accountN = Connet.testAccount();
            if(accountN == -1)
            {
                Console.Write("It seems you must install sql file. Do you want continue? (Y/N)");
                string answer = Console.ReadLine();
                answer.ToLower();
                if(answer == "y" || answer == "yes")
                {
                    SQLWorks.Setup();
                }
                else
                {
                    Console.WriteLine("Aboted. Program will exit");
                    Console.ReadLine();
                    Environment.Exit(1);
                }

            }
            else
            {
                Console.WriteLine("Accounts Detected : " + accountN.ToString());
            }
             
            TcpListener listner = new TcpListener(IPAddress.Any, 443);
            listner.Start();
            Console.WriteLine("Server Started:");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            X509Certificate2 certificate = new X509Certificate2("c:/users/andrew/sample.pfx","1");
            while (true)
            {
                TcpClient clientsock = listner.AcceptTcpClient();
                proccess(clientsock,certificate);

            }
        }
        static bool CheckSettingsExists()
        {
            if (File.Exists("conf/mysql.json") == true)
            {
                return true;
            }
            else if (Directory.Exists("conf") == false)
            {
                Directory.CreateDirectory("conf");

                return false;
            }
            else if (File.Exists("conf/mysql.json") == false)
            {

                return false;
            }
            else
            {
                return false;
            }
        }
        static bool Setup()
        {
            Console.WriteLine("===============================");
            Console.WriteLine("Setup Program Started");
            Console.WriteLine("===============================");
            return false;
        }
        static void proccess(TcpClient client, X509Certificate2 cert)
        {
            CommServerLibrary.DataReturn datas = new CommServerLibrary.DataReturn();
            SslStream ssl = new SslStream(client.GetStream(), false);
            try
            {
                ssl.AuthenticateAsServer(cert, false, SslProtocols.Tls12, true);
                DisplaySecurityLevel(ssl);
                DisplaySecurityServices(ssl);
                DisplayCertificateInformation(ssl);
                DisplayStreamProperties(ssl);
                string Message = ReadMessage(ssl);
                string returned = datas.dataReturn(Message);
                byte[] message = Encoding.UTF8.GetBytes(BuildString(returned));
                ssl.Write(message);
                Console.WriteLine("{0}", Message);
                //message = Encoding.UTF8.GetBytes(BuildString(data));
               // ssl.Write(message);
                ssl.Close();
                client.Close();
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e.Message);
                ssl.Close();
                client.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                ssl.Close();
                client.Close();
            }
            finally
            {
                ssl.Close();
                client.Close();
      
            }
        }

        static string ReadMessage(SslStream sslStream)
        {
            // from MSDN
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }
        static string BuildString(string data)
        {
            byte[] leng = new byte[1024];
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("HTTP/1.1 200 OK");
            sb.AppendLine("Date: " + DateTime.Now.ToUniversalTime().ToString("r"));
            sb.AppendLine("Content-Type: text/html; charset=UTF-8");
            //sb.AppendLine("Transfer-Encoding: chunked");
            sb.AppendLine("Connection: keep-alive");
            leng = Encoding.UTF8.GetBytes(data);
            int length = leng.Length;
            length = length + 2;
            sb.AppendLine("Content-Length: "+length);
            sb.AppendLine("");
            sb.AppendLine(data);
            return sb.ToString();
        }



        #region Display
        static void DisplaySecurityLevel(SslStream stream)
        {
            Console.WriteLine("Cipher: {0} strength {1}", stream.CipherAlgorithm, stream.CipherStrength);
            Console.WriteLine("Hash: {0} strength {1}", stream.HashAlgorithm, stream.HashStrength);
            Console.WriteLine("Key exchange: {0} strength {1}", stream.KeyExchangeAlgorithm, stream.KeyExchangeStrength);
            Console.WriteLine("Protocol: {0}", stream.SslProtocol);
        }
        static void DisplaySecurityServices(SslStream stream)
        {
            Console.WriteLine("Is authenticated: {0} as server? {1}", stream.IsAuthenticated, stream.IsServer);
            Console.WriteLine("IsSigned: {0}", stream.IsSigned);
            Console.WriteLine("Is Encrypted: {0}", stream.IsEncrypted);
        }
        static void DisplayStreamProperties(SslStream stream)
        {
            Console.WriteLine("Can read: {0}, write {1}", stream.CanRead, stream.CanWrite);
            Console.WriteLine("Can timeout: {0}", stream.CanTimeout);
        }
        static void DisplayCertificateInformation(SslStream stream)
        {
            Console.WriteLine("Certificate revocation list checked: {0}", stream.CheckCertRevocationStatus);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                Console.WriteLine("Local cert was issued to {0} and is valid from {1} until {2}.",
                    localCertificate.Subject,
                    localCertificate.GetEffectiveDateString(),
                    localCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Local certificate is null.");
            }
            // Display the properties of the client's certificate.
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                Console.WriteLine("Remote cert was issued to {0} and is valid from {1} until {2}.",
                    remoteCertificate.Subject,
                    remoteCertificate.GetEffectiveDateString(),
                    remoteCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Remote certificate is null.");
            }
        }
        private static void DisplayUsage()
        {
            Console.WriteLine("To start the server specify:");
            Console.WriteLine("serverSync certificateFile.cer");
            Environment.Exit(1);
        }
        #endregion
    }
}
