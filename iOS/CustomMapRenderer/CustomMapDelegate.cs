using System;
using System.Collections.Generic;

using MapKit;
using UIKit;
using CoreGraphics;

using Xamarin.Forms.Maps;
using dpark.CustomRenderer;

namespace dpark.iOS.CustomMapRenderer
{
    public class CustomMapDelegate : MKMapViewDelegate
    {
        List<CustomPin> _pins;

        public CustomMapDelegate(List<CustomPin> pins) { _pins = pins; }

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;
            MKPointAnnotation anno = null;

            if (annotation is MKUserLocation)
            {
                return null;
            }
            else
            {
                anno = annotation as MKPointAnnotation;
            }

            string identifier = GetIdentifier(anno);

            if (identifier == "")
                throw new Exception("No Identifier found for pin");

            annotationView = mapView.DequeueReusableAnnotation(identifier);

            if (annotationView == null)
            {
                annotationView = new CustomMKPinAnnotationView(annotation, identifier);

                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                ((CustomMKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
                ((CustomMKPinAnnotationView)annotationView).FormsIdentifier = identifier;
                ((CustomMKPinAnnotationView)annotationView).AnimatesDrop = true;
                annotationView.CanShowCallout = true;
            }                

            return annotationView;
        }

        string GetIdentifier(MKPointAnnotation annotation)
        {
            Position annotationPosition = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in _pins)
            {
                if (pin.Pin.Position == annotationPosition)
                    return pin.Id;
            }
            return "";
        }
    }
}
