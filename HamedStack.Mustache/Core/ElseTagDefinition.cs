﻿namespace HamedStack.Mustache.Core
{
    internal sealed class ElseTagDefinition : ContentTagDefinition
    {
        public ElseTagDefinition()
            : base("else", true)
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return true;
        }

        protected override IEnumerable<string> GetClosingTags()
        {
            return new string[] { "if" };
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[0];
        }
    }
}
