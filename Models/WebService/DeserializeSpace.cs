using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dpark.Models.WebService
{
    public class DeserializeSpace
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string URL { get; set; }
        public string featured_image { get; set; }
        public List<Metadata2> metadata { get; set; }
    }
    public class Metadata2
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }

    public class RootObject2
    {
        public List<DeserializeSpace> posts { get; set; }
    }
}
