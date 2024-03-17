namespace HamedStack.Mustache.Core
{
    internal interface IGenerator
    {
        void GetText(TextWriter writer, Scope keyScope, Scope contextScope, Action<Substitution> postProcessor);
    }
}
