using HamedStack.Mustache.Core;

namespace HamedStack.Mustache.Tags
{
    public class UpperTagDefinition : InlineTagDefinition
    {
        public UpperTagDefinition()
                    : base("upper")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new[] { new TagParameter("param") { IsRequired = true } };
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write(arguments["param"].ToString().ToUpperInvariant());
        }
    }
}
