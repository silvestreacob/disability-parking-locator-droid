using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dpark.Models.WebService
{
    public class Post
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string URL { get; set; }
        public string featured_image { get; set; }
        public List<Metadata> metadata { get; set; }
    }
    public class Metadata
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
    public class RootObject
    {
        public int found { get; set; }
        public List<Post> posts { get; set; }
    }
}
