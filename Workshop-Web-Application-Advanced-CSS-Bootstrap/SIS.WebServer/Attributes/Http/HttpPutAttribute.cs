using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;

namespace SIS.MvcFramework.Attributes
{
    public class HttpPutAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Put;
    }
}
