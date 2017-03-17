using System;
using System.Collections.Generic;
using CoreGraphics;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using dpark.CustomRenderer;
using dpark.iOS.Renderer;

//[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace dpark.iOS.Renderer
{
    public class CustomMapRenderer : MapRenderer
    {
        List<CustomPin> _pins;
        public CustomMapRenderer(List<CustomPin> pins)
        {
            _pins = pins;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            var formsMap = (CustomMap)Element;
            var nativeMap = Control as MKMapView;

            if (e.OldElement == null)
            {
                //nativeMap.Delegate = new CustomMapRenderer(formsMap.CustomPins as List<CustomPin>);
                nativeMap.GetViewForAnnotation = UpdatePins;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
            }
        }

        //MKAnnotationView UpdatePins(MKMapView mapView, IMKAnnotation annotation)
        //{
        //    MKAnnotationView annotationView = null;
        //    try
        //    {
        //        var formsMap = (CustomMap)Element;
        //        var nativeMap = Control as MKMapView;

        //        foreach(var item in formsMap.CustomPins)
        //        {
        //            var anno = annotation as MKPointAnnotation;

        //            annotationView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(item.Id);
        //            if (annotationView == null)
        //            {
        //                annotationView = new CustomMKAnnotationView(anno, item.Id);
        //                ((CustomMKAnnotationView)annotationView).Id = item.Id;
        //                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
        //                ((MKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
        //                ((MKPinAnnotationView)annotationView).AnimatesDrop = true;
        //                annotationView.CanShowCallout = true;

        //            }
        //            //formsMap.Pins.Add(item.Pin);
        //            return annotationView;
        //        }

        //    }
        //    catch { }

        //    return null;
        //}

        MKAnnotationView UpdatePins(MKMapView mapView, IMKAnnotation annotation)
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

            annotationView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(identifier);

            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(anno, identifier);
                ((CustomMKAnnotationView)annotationView).Id = identifier;
                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                ((MKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
                ((MKPinAnnotationView)annotationView).AnimatesDrop = true;
                annotationView.CanShowCallout = true;
            }
      
            return annotationView;
                
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            var formsMap = (CustomMap)Element;
            //var customView = e.View as MKPinAnnotationView;
            var customView = e.View as CustomMKAnnotationView;

            if (!string.IsNullOrWhiteSpace(customView.Id))
            {
                formsMap.ShowPinDetailInfo(customView.Id);
            }
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
