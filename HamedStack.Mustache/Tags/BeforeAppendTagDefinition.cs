﻿

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using HamedStack.Mustache.Core;

namespace HamedStack.Mustache.Tags
{
    internal sealed class BeforeAppendTagDefinition : ContentTagDefinition
    {
        private const string beforeAppend = "beforeappendtext";
        private static readonly TagParameter beforeAppendParameter = new TagParameter(beforeAppend) { IsRequired = true };

        public BeforeAppendTagDefinition()
                    : base("beforeappend")
        {
        }

        public override IEnumerable<NestedContext> GetChildContext(TextWriter writer, Scope keyScope, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("beforeappend", out var appendable))
            {
                if (!string.Equals(appendable.ToString(), "true", System.StringComparison.InvariantCultureIgnoreCase))
                    return new List<NestedContext>();
                return base.GetChildContext(writer, keyScope, arguments, contextScope);
            }
            return base.GetChildContext(writer, keyScope, arguments, contextScope);

        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new List<TagParameter>() { beforeAppendParameter };
        }
    }
}
