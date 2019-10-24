using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace SIS.MvcFramework.ViewEngine
{
    public class SisViewEngine : IViewEngine
    {
        public string GetHtml<T>(string viewContent, T model)
        {
            string cSharpHtmlCode = GetCSharpCode(viewContent);
            string code = $@"
                   using System;
                   using System.Collections.Generic;
                   using System.Linq;
                   using System.Text;
                   using SIS.MvcFramework.ViewEngine;

                   namespace AppViewCodeNamespace
                   {{
                       public class AppViewCode : IView
                       {{
                           public string GetHtml()
                           {{
                               var html = new StringBuilder();
                   
                                {cSharpHtmlCode}

                               return html.ToString();
                           }}
                       }}
                   }}
                   ";

            var view = CompileAndInstance(code);
            var htmlResult = view?.GetHtml();
            return htmlResult;
        }

        private IView CompileAndInstance(string code)
        {
            var compilation = CSharpCompilation.Create("AppViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

            var netStandartAssembly = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();

            foreach (var assembly in netStandartAssembly)
            {
                compilation = compilation.AddReferences(MetadataReference
                    .CreateFromFile(Assembly.Load(assembly).Location));
            }

            return null;
        }

        private string GetCSharpCode(string viewContent)
        {
            return string.Empty;
        }
    }
}
