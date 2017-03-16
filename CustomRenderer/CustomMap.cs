using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace dpark.CustomRenderer
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }
}
