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
using System.Security.Authentication;

namespace CommServer
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            HttpListener http = new HttpListener();
            http.
            TcpListener listner = new TcpListener(IPAddress.Any, 443);
            listner.Start();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            X509Certificate2 certificate = new X509Certificate2("c:/users/andrew/sample.pfx","1");
            while (true)
            {
                TcpClient clientsock = listner.AcceptTcpClient();
                proccess(clientsock,certificate);
            }
        }
        static void proccess(TcpClient client, X509Certificate2 cert)
        {
            SslStream ssl = new SslStream(client.GetStream(), false);
            try
            {
                ssl.AuthenticateAsServer(cert, false, SslProtocols.Tls12, true);
                DisplaySecurityLevel(ssl);
                DisplaySecurityServices(ssl);
                DisplayCertificateInformation(ssl);
                DisplayStreamProperties(ssl);
                byte[] message = Encoding.UTF8.GetBytes(BuildString());
                ssl.Write(message);
                string Message = ReadMessage(ssl);
                Console.WriteLine("{0}", Message);
             message = Encoding.UTF8.GetBytes(BuildString());
                ssl.Write(message);
            }
            catch (AuthenticationException e)
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
        static byte[] Send(string Message)
        {
            byte[] atk = Encoding.UTF8.GetBytes(Message);
            return atk;
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
        static string BuildString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("HTTP/1.1 200 OK");
            //sb.AppendLine("Date: " + DateTime.Now.ToString());
            sb.AppendLine("Content-Type: text/html; charset=UTF-8");
            sb.AppendLine("Transfer-Encoding: chunked");
            sb.AppendLine("Connection: keep-alive");
            sb.AppendLine("");
            sb.AppendLine("WARNING");
            return sb.ToString();
        }




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
    }
}
