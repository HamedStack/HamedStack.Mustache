using HamedStack.Mustache.Core;

namespace HamedStack.Mustache.Tags
{
    public class TabTagDefinition : InlineTagDefinition
    {
        public TabTagDefinition()
                    : base("tab")
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write("\t");
        }
    }
}
