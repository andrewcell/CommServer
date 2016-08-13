using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace CommServer
{
    class Program
    {
        static void Main(string[] args)
        {   
            TcpListener listner = new TcpListener(IPAddress.Any, 443);
            listner.Start();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            TcpClient clientsock = listner.AcceptTcpClient();
            X509Certificate2 certificate = new X509Certificate2("c:/users/andrew/sample.pfx","1");
            using (SslStream ssl = new SslStream(clientsock.GetStream()))
            {
                ssl.AuthenticateAsServer(certificate);
                Console.WriteLine(ReadMessage(ssl));
            }
            Console.Beep();
        }
        static string ReadMessage(SslStream sslStream)
        {
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
    }
}
