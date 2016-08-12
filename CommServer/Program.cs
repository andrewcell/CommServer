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
            StreamWriter sw = new StreamWriter();

            using (SslStream ssl = new SslStream(clientsock.GetStream()))
            {
                ssl.AuthenticateAsServer(certificate);
           
            }
        }
    }
}
