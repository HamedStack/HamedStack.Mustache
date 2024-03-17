 
namespace HamedStack.Mustache.Core
{
    internal sealed class IfTagDefinition : ConditionTagDefinition
    {
        public IfTagDefinition()
            : base("if")
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return false;
        }
    }
}
