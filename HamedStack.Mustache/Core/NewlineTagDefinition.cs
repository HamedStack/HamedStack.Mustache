namespace HamedStack.Mustache.Core
{
    internal sealed class NewlineTagDefinition : InlineTagDefinition
    {
        public NewlineTagDefinition()
            : base("newline")
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write(Environment.NewLine);
        }
    }
}
