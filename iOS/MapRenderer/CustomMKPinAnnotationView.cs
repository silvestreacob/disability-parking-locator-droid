using MapKit;
using UIKit;
using CoreGraphics;

namespace dpark.iOS.MapRenderer
{
    public class CustomMKPinAnnotationView : MKPinAnnotationView
    {
        public CustomMKPinAnnotationView(IMKAnnotation annotation, string annotationId) : base(annotation, annotationId) { }

        public string FormsIdentifier { get; set; }


    }
}
