 
namespace HamedStack.Mustache.Core
{
    public sealed class Context
    {
        internal Context(string tagName, ContextParameter[] parameters)
        {
            TagName = tagName;
            Parameters = parameters;
        }

        public string TagName { get; }

        public ContextParameter[] Parameters { get; }
    }
}
