using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            this.headers.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            bool isContained = true;

            if (!headers.ContainsKey(key))
            {
                isContained = false;
            }

            return isContained;
        }

        public HttpHeader GetHeader(string key)
        {
            if (headers.ContainsKey(key))
            {
                return (HttpHeader)headers.First(h => h.Key == key).Value;
            }

            return null;
        }

        public override string ToString()
        {
            return base.ToString(); 
        }
    }
}
