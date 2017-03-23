using Xamarin.Forms;

using dpark.CustomRenderer;
using dpark.ViewModels.MapSearch;
using dpark.iOS.CustomMapRenderer;

using MapKit;
using UIKit;
using System.Collections.Generic;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.Maps.iOS;
using System;
using Xamarin.Forms.Maps;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(CustomMap), typeof(MapCustomRenderer))]

namespace dpark.iOS.CustomMapRenderer
{
    public class MapCustomRenderer : Xamarin.Forms.Maps.iOS.MapRenderer
    {
        //List<CustomPin> _pins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidDeselectAnnotationView +=OnDidDeselectAnnotationView;
            }
        }
        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;
            MKPointAnnotation anno = null;

            if (annotation is MKUserLocation) { return null; }
            else { anno = annotation as MKPointAnnotation; }

            string identifier = GetIdentifier(anno);
            if (identifier == "")
            {
                annotationView = new CustomMKPinAnnotationView(annotation, identifier);
                
                //annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                ((CustomMKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Red;
                ((CustomMKPinAnnotationView)annotationView).FormsIdentifier = identifier;
                ((CustomMKPinAnnotationView)annotationView).AnimatesDrop = true;
                annotationView.CanShowCallout = true;
                //throw new Exception("No Identifier found for pin");
            }
            else
            {
                annotationView = mapView.DequeueReusableAnnotation(identifier);
                if (annotationView == null)
                {
                    annotationView = new CustomMKPinAnnotationView(annotation, identifier);

                    //annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.LoadFromData("http://dpark.us/wp-content/uploads/2014/09/image1-125x125.jpg"));
                    annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                    ((CustomMKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
                    ((CustomMKPinAnnotationView)annotationView).FormsIdentifier = identifier;
                    ((CustomMKPinAnnotationView)annotationView).AnimatesDrop = true;
                    annotationView.CanShowCallout = true;
                }
            }
            return annotationView;
        }

        string GetIdentifier(MKPointAnnotation annotation)
        {
            try
            {
                var formsMap = (CustomMap)Element;
                Position annotationPosition = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);

                foreach (var item in formsMap.CustomPins)
                {
                    if (item.Pin.Position == annotationPosition)
                    {
                        return item.Id;
                    }
                }
            }
            catch { return ""; }

            return "";

        }
        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            var formsMap = (CustomMap)Element;
            var customView = e.View as CustomMKPinAnnotationView;

            if (!string.IsNullOrWhiteSpace(customView.FormsIdentifier))
            {
                formsMap.ShowPinDetailInfo(customView.FormsIdentifier);
                //return;               
            }
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKPinAnnotationView;

            if (!e.View.Selected)
            {
                //customPinView.RemoveFromSuperview();
                customView.Selected = false;
                customView.Dispose();
                customView = null;
            }
        }

    }
}
