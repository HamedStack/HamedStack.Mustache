namespace HamedStack.Mustache.Core
{
    public class KeyFoundEventArgs : EventArgs
    {
        internal KeyFoundEventArgs(string key, object value, bool isExtension)
        {
            Key = key;
            Substitute = value;
        }

        public string Key { get; }

        public bool IsExtension { get; }

        public object Substitute { get; set; }
    }
}
