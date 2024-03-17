﻿namespace HamedStack.Mustache.Core
{
    internal sealed class IndexTagDefinition : InlineTagDefinition
    {
        public IndexTagDefinition()
            : base("index", true)
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("index", out object index))
            {
                writer.Write(index);
            }
        }
    }
}
