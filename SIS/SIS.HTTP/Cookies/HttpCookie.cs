using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime dateTime { get; set; }

        public string Path { get; set; }

        public bool IsNew { get; set; }

        public bool HttpOnly { get; set; }

    }
}