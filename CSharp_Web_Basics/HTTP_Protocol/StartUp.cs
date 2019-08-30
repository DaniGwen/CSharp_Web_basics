using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HTTP_Protocol
{
    class Program
    {
        static void Main(string[] args)
        {
            string newLine = "\r\n";

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 12345);

            tcpListener.Start();

            while (true)
            {
                //Waits for web client to connect
                TcpClient client = tcpListener.AcceptTcpClient();


                using (NetworkStream stream = client.GetStream())
                {
                    //Reading the clients stream of bytes
                    //byte array should be 1024b for effective memory utilization
                    byte[] requestBytes = new byte[100000];
                    int readBytes = stream.Read(requestBytes, 0, requestBytes.Length);
                    string stringRequest = Encoding.UTF8.GetString(requestBytes, 0, readBytes);

                    //Display client request 
                    Console.WriteLine(new string('=', 70));
                    Console.WriteLine(stringRequest);

                    //Responding back to the web client
                    string responceBody = "<h1>Hello, user</h1>";

                    string responce = "HTTP/1.0 200 OK" + newLine +
                        "Content-Type: text/html" + newLine +
                        "Server: MyCustomServer/1.0" + newLine +
                        $"Content-Length: {responceBody.Length}" + newLine + newLine +
                        responceBody;
                    byte[] responceBytes = Encoding.UTF8.GetBytes(responce);
                    stream.Write(responceBytes, 0, responceBytes.Length);
                }


            }
        }
    }
}
