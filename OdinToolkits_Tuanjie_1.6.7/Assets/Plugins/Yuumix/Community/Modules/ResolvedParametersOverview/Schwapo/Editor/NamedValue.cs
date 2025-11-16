namespace Yuumix.Community.Schwapo.Editor
{
    public class NamedValue
    {
        public string Description;
        public string Name;
        public string Type;

        public NamedValue(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
