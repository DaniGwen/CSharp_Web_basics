﻿using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses.Contracts;
using SIS.HTTP.Extensions;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
            this.Cookies = new HttpCookieCollection();
        }

        public IHttpCookieCollection Cookies { get; set; }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public byte[] Content { get; set; }

        public void AddCookie(HttpCookie httpCookie)
        {
            CoreValidator.ThrowIfNull(httpCookie, nameof(httpCookie));

            this.Cookies.AddCookie(httpCookie);
        }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            byte[] httpResponseBytesWithoutBody = Encoding.UTF8.GetBytes(this.ToString());
            byte[] httpResponseBytesWithBody = new byte[httpResponseBytesWithoutBody.Length + this.Content.Length];

            for (int i = 0; i < httpResponseBytesWithoutBody.Length; i++)
            {
                httpResponseBytesWithBody[i] = httpResponseBytesWithoutBody[i];
            }

            for (int i = 0; i < httpResponseBytesWithBody.Length - httpResponseBytesWithoutBody.Length; i++)
            {
                httpResponseBytesWithBody[i + httpResponseBytesWithoutBody.Length] = this.Content[i];
            }

            return httpResponseBytesWithBody;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result
                .Append($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetResponseCode()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(this.Headers)
                .Append(GlobalConstants.HttpNewLine);

            if (this.Cookies.HasCookies())
            {
                result.Append($"{this.Cookies}").Append(GlobalConstants.HttpNewLine);
            }
            result.Append(GlobalConstants.HttpNewLine);

            return result.ToString();
        }
    }
}
