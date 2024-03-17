 
namespace HamedStack.Mustache.Core
{
    public abstract class ContentTagDefinition : TagDefinition
    {
        protected ContentTagDefinition(string tagName)
            : base(tagName)
        {
        }

        internal ContentTagDefinition(string tagName, bool isBuiltin)
            : base(tagName, isBuiltin)
        {
        }

        protected override bool GetHasContent()
        {
            return true;
        }
    }
}
