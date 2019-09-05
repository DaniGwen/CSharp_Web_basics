using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using System;

namespace Demo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //string request = "POST /url/asd?name=Sinthia&id=3#fragment HTTP/1.1\r\n"
            //     + "Authorization: Basic 234353543\r\n"
            //     + "Date: " + DateTime.Now + "\r\n"
            //     + "Host: localhost:6000\r\n"
            //     + "\r\n"
            //     + "username=SinthiaRothrock$password=4123";

            //HttpRequest httpRequest = new HttpRequest(request);

            var statusCode = HttpResponseStatusCode.BadRequest;

            Console.WriteLine((int)statusCode);
            ;
        }
    }
}

