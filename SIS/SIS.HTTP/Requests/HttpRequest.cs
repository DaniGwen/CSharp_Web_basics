using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.Headers = new HttpHeaderCollection();
            this.QueryData = new Dictionary<string, object>();
            this.FormData = new Dictionary<string, object>();
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLineParams)
        {
            if (requestLineParams.Length != 3 || requestLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            throw new NotImplementedException();
        }
        private void ParseRequest(string requestString)
        {
            string[] splitRequestString = requestString
                .Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLineParams = splitRequestString[0]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLineParams);
            this.ParseRequestUrl(requestLineParams);
            this.ParseRequestPath();

            this.ParseHeaders(splitRequestString.Skip(1).ToArray());
            //this.ParseCookies()
            this.ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);
        }

        private void ParseRequestParameters(string v)
        {
            throw new NotImplementedException();
        }

        private void ParseHeaders(string[] v)
        {
            throw new NotImplementedException();
        }

        private void ParseRequestPath()
        {
            throw new NotImplementedException();
        }

        private void ParseRequestUrl(string[] requestLineParams)
        {
            throw new NotImplementedException();
        }

        private void ParseRequestMethod(string[] requestLineParams)
        {
            HttpRequestMethod method;
            bool parseResult = HttpRequestMethod.TryParse(requestLineParams[0], out method);

            if (!parseResult)
            {
                throw new BadRequestException();
            }

            this.RequestMethod = method;
        }
    }
}
