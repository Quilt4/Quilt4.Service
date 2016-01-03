namespace Quilt4.Service.Entity
{
    public class Setting
    {
        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}