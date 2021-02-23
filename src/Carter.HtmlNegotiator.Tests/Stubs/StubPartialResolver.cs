using System.Collections.Generic;
using HandlebarsDotNet;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubPartialResolver : IPartialTemplateResolver
    {
        private readonly Dictionary<string, string> partials;

        public StubPartialResolver(Dictionary<string, string> partials)
        {
            this.partials = partials;
        }

        public bool TryRegisterPartial(IHandlebars env, string partialName, string templatePath)
        {
            if (partials.TryGetValue(partialName, out var partial))
            {
                env.RegisterTemplate(partialName, partial);
                return true;
            }
            return false;
        }
    }
}