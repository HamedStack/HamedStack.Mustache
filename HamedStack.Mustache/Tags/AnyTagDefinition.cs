using System.Collections;
using HamedStack.Mustache.Core;

namespace HamedStack.Mustache.Tags
{
    public class AnyTagDefinition : ContentTagDefinition
    {
        private const string ConditionParameter = "condition";

        public AnyTagDefinition()
                    : base("Any")
        { }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[0];
        }

        protected override bool GetIsContextSensitive()
        {
            return false;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new[] { new TagParameter(ConditionParameter) { IsRequired = true } };
        }

        private bool IsConditionSatisfied(object condition)
        {
            return condition is IEnumerable enumerable && enumerable.Cast<object>().Any();
        }

        public override bool ShouldGeneratePrimaryGroup(Dictionary<string, object> arguments)
        {
            var condition = arguments[ConditionParameter];
            return IsConditionSatisfied(condition);
        }
    }
}
