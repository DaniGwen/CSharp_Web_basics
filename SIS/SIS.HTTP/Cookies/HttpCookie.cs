using SIS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie
    {
        private const int HttpCookieDefaultExpirationDay = 3;

        private const string HttpCookieDefaultPath = "/";

        public HttpCookie(string key, string value, int expires = HttpCookieDefaultExpirationDay, string path = HttpCookieDefaultPath) : this(key, value, true, expires, path)
        {
             
        }

        public HttpCookie(string key, string value, bool isNew, int expires = HttpCookieDefaultExpirationDay, string path = HttpCookieDefaultPath)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Key = key;
            this.Value = value;
            this.Path = path;
            this.Expires = DateTime.UtcNow.AddDays(expires);
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime Expires { get; private set; }

        public string Path { get; set; }

        public bool IsNew { get; set; }

        public bool HttpOnly { get; set; } = true;

        public void Delete()
        {
            this.Expires = DateTime.UtcNow.AddDays(-1);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"{this.Key}= {this.Value}; Expires={this.Expires:R}");

            if (this.HttpOnly)
            {
                stringBuilder.Append("; HttpOnly");
            }

            stringBuilder.Append($"; path={this.Path}");

            return stringBuilder.ToString();
        }
    }
}