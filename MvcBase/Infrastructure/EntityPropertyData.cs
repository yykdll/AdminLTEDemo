using System.ComponentModel;

namespace MvcBase.Infrastructure
{
    public class EntityPropertyData
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public object NewValue { get; set; }

        public object OldValue { get; set; }

    }
}
