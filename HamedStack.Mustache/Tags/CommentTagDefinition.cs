using HamedStack.Mustache.Core;

namespace HamedStack.Mustache.Tags
{
    public class CommentTagDefinition : ContentTagDefinition
    {
        private const string CommentText = "commenttext";
        private static readonly TagParameter CommentParameter = new TagParameter(CommentText) { IsRequired = true };

        public CommentTagDefinition()
                    : base("comment")
        {
        }

        public override IEnumerable<NestedContext> GetChildContext(TextWriter writer, Scope keyScope, Dictionary<string, object> arguments, Scope contextScope)
        {
            return new List<NestedContext>();
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new List<TagParameter>() { CommentParameter };
        }
    }
}
