using SIS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        public string Id { get; }

        private readonly Dictionary<string, object> SessionParameters;

        public HttpSession(string id)
        {
            this.Id = id;

            this.SessionParameters = new Dictionary<string, object>();
        }

        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            this.SessionParameters[name] = parameter;
        }

        public void ClearParameter()
        {
            this.SessionParameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return SessionParameters.ContainsKey(name);
        }

        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this.SessionParameters[name]; 
        }
    }
}
