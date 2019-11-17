using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoFluentPWA.ServiceModel
{
    public class MongoConnectionSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string CollectionName { get; set; }
    }
}
