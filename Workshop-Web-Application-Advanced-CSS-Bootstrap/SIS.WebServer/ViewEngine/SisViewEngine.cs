﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SIS.MvcFramework.ViewEngine
{
    public class SisViewEngine : IViewEngine
    {
        private string GetModelType<T>(T model)
        {
            if (model is Enumerable)
            {
                return $"IEnumerable<{model.GetType().GetGenericArguments()[0].FullName}>";
            }
            return model.GetType().FullName;
        }

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
                           public string GetHtml(object model)
                           {{
                               var Model = {(model == null ? "new {}" : "model as " + GetModelType(model))};
                               var html = new StringBuilder();
                   
                                {cSharpHtmlCode}

                               return html.ToString();
                           }}
                       }}
                   }}
                   ";

            var view = this.CompileAndInstance(code, model?.GetType().Assembly);
            var htmlResult = view?.GetHtml(model);
            return htmlResult;
        }

        private IView CompileAndInstance(string code, Assembly modelAssembly)
        {
            modelAssembly = modelAssembly == null ? Assembly.GetEntryAssembly() : modelAssembly;

            var compilation = CSharpCompilation.Create("AppViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(Assembly.GetEntryAssembly().Location))
                .AddReferences(MetadataReference.CreateFromFile(modelAssembly.Location));

            var netStandartAssembly = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();

            foreach (var assembly in netStandartAssembly)
            {
                compilation = compilation.AddReferences(MetadataReference
                    .CreateFromFile(Assembly.Load(assembly).Location));
            }

            compilation = compilation.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(code));
            using (var memoryStream = new MemoryStream())
            {
                var compilationResult = compilation.Emit(memoryStream);

                if (!compilationResult.Success)
                {
                    foreach (var error in compilationResult.Diagnostics) //.Where(x=>x.Severity == DiagnosticSeverity.Error)
                    {
                        System.Console.WriteLine(error.GetMessage());
                    }
                    return null;
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                var assemblyBytes = memoryStream.ToArray();
                var assembly = Assembly.Load(assemblyBytes);

                var type = assembly.GetType("AppViewCodeNamespace.AppViewCode");
                if (type == null)
                {
                    System.Console.WriteLine("AppViewCode not found!");
                    return null;
                }

                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    Console.WriteLine("AppViewCode can not be instantiated");
                    return null;
                }
                return instance as IView;
            }
        }

        private string GetCSharpCode(string viewContent)
        {
            string[] lines = viewContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var csharpCode = new StringBuilder();
            var supportedOperators = new[] { "for", "if", "else" };

            foreach (var line in lines)
            {
                if (line.TrimStart().StartsWith("{") || line.TrimStart().StartsWith("}"))
                {
                    // { / }
                    csharpCode.Append(line);
                }
                else if (supportedOperators.Any(x => line.TrimStart().StartsWith("@" + x)))
                {
                    // @C#
                    var atSignLocation = line.IndexOf("@");
                    var cSharpLine = line.Remove(atSignLocation, 1);
                    csharpCode.AppendLine(cSharpLine);
                }
                else
                {
                    //HTML
                    if (!line.Contains("@"))
                    {
                        var cSharpLine = $"html.AppendLine(@\"{line.Replace("\"", "\"\"")}\");";
                        csharpCode.AppendLine(cSharpLine);
                    }
                    else if (line.Contains("@RenderBody()"))
                    {
                        var csharpLine = $"html.AppendLine(@\"{line}\");";
                        csharpCode.AppendLine(csharpLine);
                    }
                    else
                    {
                        var cSharpStringToAppend = "html.AppendLine(@\"";
                        var restOfLine = line;
                        while (restOfLine.Contains("@"))
                        {
                            var atSignLocation = restOfLine.IndexOf("@");
                            var plainText = restOfLine.Substring(0, atSignLocation);
                            Regex cSharpCodeRegex = new Regex(@"[^\s<""]+", RegexOptions.Compiled);
                            var cSharpExpression = cSharpCodeRegex.Match(restOfLine)?.Value;
                            cSharpStringToAppend += plainText + "\" + " + cSharpExpression + " + @\"";

                            restOfLine = restOfLine.Substring(atSignLocation + cSharpExpression.Length + 1);
                        }
                        cSharpStringToAppend += $"{restOfLine}\");";
                        csharpCode.AppendLine(cSharpStringToAppend);
                    }
                }
            }
            return csharpCode.ToString();
        }
    }
}
