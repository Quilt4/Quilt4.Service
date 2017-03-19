using System;

namespace Quilt4.Service.Entity
{
    public class Application
    {
        public Application(Guid applicationKey, string name)
        {
            ApplicationKey = applicationKey;
            Name = name;
        }

        public Guid ApplicationKey { get; }
        public string Name { get; }
    }
}