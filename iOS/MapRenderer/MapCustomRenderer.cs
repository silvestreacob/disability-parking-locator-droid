using Xamarin.Forms;

using dpark.CustomRenderer;
using dpark.iOS.MapRenderer;

using MapKit;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(CustomMap), typeof(MapCustomRenderer))]

namespace dpark.iOS.MapRenderer
{
    public class MapCustomRenderer : Xamarin.Forms.Maps.iOS.MapRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                nativeMap.Delegate = new CustomMapDelegate(formsMap.CustomPins as List<CustomPin>);
            }
        }
    }
}
