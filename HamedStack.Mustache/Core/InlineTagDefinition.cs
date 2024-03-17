﻿namespace HamedStack.Mustache.Core
{
    public abstract class InlineTagDefinition : TagDefinition
    {
        protected InlineTagDefinition(string tagName)
            : base(tagName)
        {
        }

        internal InlineTagDefinition(string tagName, bool isBuiltin)
            : base(tagName, isBuiltin)
        {
        }

        protected override bool GetHasContent()
        {
            return false;
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[0];
        }
    }
}
