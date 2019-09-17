using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie
    {
        private const int HttpCookieDefaultExpirationDay = 3;

        private const string HttpCookieDefaultPath = "/";

        public HttpCookie(string key, string value, int expires, string path)
        {
            expires = HttpCookieDefaultExpirationDay;
            path = HttpCookieDefaultPath;
        }

        public HttpCookie(string key, string value, bool isNew, int expires, string path)
        {
            expires = HttpCookieDefaultExpirationDay;
            path = HttpCookieDefaultPath;
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime Expires { get; private set; }

        public string Path { get; set; }

        public bool IsNew { get; set; }

        public bool HttpOnly { get; set; } = true;

        public void Delete()
        {

        }

    }
}