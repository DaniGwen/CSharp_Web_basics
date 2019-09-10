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

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool HasQueryString()
        {
            return this.Url.Split('?').Length > 1;
        }
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
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));

            return true;  //TODO Regex query string
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

            this.ParseRequestHeaders(this.ParsePlainRequestHeaders(splitRequestString).ToArray());
            //this.ParseCookies()
            this.ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);
        }

        private void ParseRequestQueryParameters()
        {
            if (this.HasQueryString())
            {
                this.Url
                .Split('?', '#')[1]
                .Split('&')
                .Select(queryParameter => queryParameter.Split('='))
                .ToList()
                .ForEach(queryKeyValuePair => this.QueryData.Add(queryKeyValuePair[0], queryKeyValuePair[1]));
            }
        }

        

        private void ParseRequestFormDataParameters(string requestBody)
        {
            if (!string.IsNullOrEmpty(requestBody))
            {
                requestBody
                .Split('&')
                .Select(queryParameter => queryParameter.Split('='))
                .ToList()
                .ForEach(queryKeyValuePair => this.FormData.Add(queryKeyValuePair[0], queryKeyValuePair[1]));
            } 
            //TODO: Parse multiple parameters by Name 
        }
        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(requestBody);
        }

        private void ParseRequestHeaders(string[] plainHeaders)
        {
            plainHeaders.Select(plainHeader => plainHeader.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToList()
                .ForEach(headerKeyValuePair => this.Headers.AddHeader(new HttpHeader
                (
                   headerKeyValuePair[0],
                   headerKeyValuePair[1]
                )));
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }

        private void ParseRequestUrl(string[] requestLineParams)
        {
            this.Url = requestLineParams[1];
        }

        private void ParseRequestMethod(string[] requestLineParams)
        {
            HttpRequestMethod method;

            bool parseResult = HttpRequestMethod.TryParse(requestLineParams[0], ignoreCase: true, out method);

            if (!parseResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, requestLineParams[0]));
            }

            this.RequestMethod = method;
        }

        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }
    }
}
