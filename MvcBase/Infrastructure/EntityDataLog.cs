using System.Collections.Generic;
using System.ComponentModel;

namespace MvcBase.Infrastructure
{
    public class EntityDataLog
    {
        public string FullName { get; set; }

        public string DisplayName { get; set; }

        public string PrimaryKeyName { get; set; }

        public object PrimaryKeyValue { get; set; }

        public List<EntityPropertyData> ChangedProperties { get; set; }


        public EntityDataLog()
        {
            this.ChangedProperties = new List<EntityPropertyData>();
        }
    }
}
