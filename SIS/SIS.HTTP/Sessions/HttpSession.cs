using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        public string Id { get; }

        private Dictionary<string, object> Sessions;

        public HttpSession(string id)
        {
            this.Sessions
        }
        public void AddParameter(string name, object parameter)
        {
            throw new NotImplementedException();
        }

        public void ClearParameter()
        {
            throw new NotImplementedException();
        }

        public bool ContainsParameter(string name)
        {
            throw new NotImplementedException();
        }

        public object GetParameter(string name)
        {
            throw new NotImplementedException();
        }
    }
}
