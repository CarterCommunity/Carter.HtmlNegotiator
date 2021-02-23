using System.Collections.Generic;
using System.IO;
using HandlebarsDotNet;
using HandlebarsDotNet.Helpers;
using HandlebarsDotNet.Runtime;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubHandlebarsEnvironment : IHandlebars
    {
        public Dictionary<string, string> Templates { get; }

        public StubHandlebarsEnvironment()
        {
            Templates = new Dictionary<string, string>();
        }
        
        public HandlebarsTemplate<TextWriter, object, object> Compile(TextReader template)
        {
            throw new System.NotImplementedException();
        }

        public HandlebarsTemplate<object, object> Compile(string template)
        {
            throw new System.NotImplementedException();
        }

        public HandlebarsTemplate<object, object> CompileView(string templatePath)
        {
            throw new System.NotImplementedException();
        }

        public HandlebarsTemplate<TextWriter, object, object> CompileView(string templatePath, ViewReaderFactory readerFactoryFactory)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterTemplate(string templateName, HandlebarsTemplate<TextWriter, object, object> template)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterTemplate(string templateName, string template)
        {
            Templates.Add(templateName, template);
        }

        public void RegisterHelper(string helperName, HandlebarsHelper helperFunction)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHelper(string helperName, HandlebarsHelperWithOptions helperFunction)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHelper(string helperName, HandlebarsReturnHelper helperFunction)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHelper(string helperName, HandlebarsReturnWithOptionsHelper helperFunction)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHelper(string helperName, HandlebarsBlockHelper helperFunction)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHelper(string helperName, HandlebarsReturnBlockHelper helperFunction)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHelper(IHelperDescriptor<BlockHelperOptions> helperObject)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHelper(IHelperDescriptor<HelperOptions> helperObject)
        {
            throw new System.NotImplementedException();
        }

        public DisposableContainer Configure()
        {
            throw new System.NotImplementedException();
        }

        public HandlebarsConfiguration Configuration { get; }
    }
}