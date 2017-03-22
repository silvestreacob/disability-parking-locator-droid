using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dpark.Models.WebService
{
    static public class Config
    {
        public const string ServerAddress = @"https://dpark.us/";
        public const string GmapAddress = @"https://maps.googleapis.com/";
        public const string GmapApikey = @"&key=AIzaSyAmTEF3KtodfibGFyTmv91_1Ll-KMJu9ak";
        public const string SecondaryGeoApiKey= @"&key=AIzaSyD3rbYCriG8zZ_SI6HZ8I6qaSqSZzLnaE4";
        public const string OnSpecificRegion = @"&components=administrative_area:HI|country:US";
    }
}
