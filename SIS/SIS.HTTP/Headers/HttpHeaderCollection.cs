using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> httpHeaders;

        public HttpHeaderCollection()
        {
            this.httpHeaders = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            this.httpHeaders.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            bool isContained = true;

            if (!httpHeaders.ContainsKey(key))
            {
                isContained = false;
            }

            return isContained;
        }

        public HttpHeader GetHeader(string key)
        {
            if (httpHeaders.ContainsKey(key))
            {
                return (HttpHeader)httpHeaders.First(h => h.Key == key).Value;
            }

            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var header in this.httpHeaders)
            {
                sb.Append($"{header.ToString()}\r\n");
            }
            return sb.ToString(); 
        }
    }
}
