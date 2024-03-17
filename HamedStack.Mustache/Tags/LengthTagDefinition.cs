

// ReSharper disable UnusedMember.Global


using HamedStack.Mustache.Core;

namespace HamedStack.Mustache.Tags
{
    internal sealed class LengthTagDefinition : InlineTagDefinition
    {
        public LengthTagDefinition()
                    : base("length", true)
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("length", out var length))
            {
                writer.Write(length);
            }
        }
    }
}
