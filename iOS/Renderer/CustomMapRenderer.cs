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

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace dpark.iOS.Renderer
{
    public class CustomMapRenderer : MapRenderer
    {
        //UIView customPinView;
        List<CustomPin> customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);            
            var formsMap = (CustomMap)Element;
            var nativeMap = Control as MKMapView;

            if (e.OldElement == null)
            {
                nativeMap.GetViewForAnnotation = UpdatePins;
            }


            //if(e.OldElement != null)
            //{
            //    var nativeMap = Control as MKMapView;
            //    nativeMap.GetViewForAnnotation = null;
            //    //nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
            //}

            //if (e.NewElement != null)
            //{
            //    var formsMap = (CustomMap)e.NewElement;
            //    var nativeMap = Control as MKMapView;
            //    customPins = formsMap.CustomPins;

            //    nativeMap.GetViewForAnnotation = GetViewForAnnotation;
            //    //nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
            //}
        }

        MKAnnotationView UpdatePins(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;
            try
            {
                var formsMap = (CustomMap)Element;
                var nativeMap = Control as MKMapView;

                foreach(var item in formsMap.CustomPins)
                {
                    var anno = annotation as MKPointAnnotation;

                    annotationView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(item.Id);
                    if (annotationView == null)
                    {
                        annotationView = new MKPinAnnotationView(anno, item.Id);
                        ((MKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
                        ((MKPinAnnotationView)annotationView).AnimatesDrop = true;
                        annotationView.CanShowCallout = true;
                    }

                    formsMap.Pins.Add(item.Pin);
                    return annotationView;
                }
                                
            }
            catch { }

            return null;
        }
        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            var anno = annotation as MKPointAnnotation;
            var customPin = GetCustomPin(anno);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            //annotationView = mapView.DequeueReusableAnnotation(customPin.Id);
            //if (annotationView == null)
            //{
            //    annotationView = new CustomMKAnnotationView(annotation, customPin.Id);
            //    annotationView.Image = UIImage.FromFile("pin.png");
            //    annotationView.CalloutOffset = new CGPoint(0, 0);
            //    annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.LoadFromData(customPin.Url));
            //    annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
            //    ((CustomMKAnnotationView)annotationView).Id = customPin.Id;
            //    ((CustomMKAnnotationView)annotationView).Url = customPin.Url;
            //}

            annotationView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(customPin.Id);
            if (annotationView == null)
            {
                annotationView = new MKPinAnnotationView(anno, customPin.Id);
                ((MKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
                ((MKPinAnnotationView)annotationView).AnimatesDrop = true;
                annotationView.CanShowCallout = true;
            }
            return annotationView;
        }

        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins)
            {
                //if (pin.Pin.Position == position)
                //{
                    return pin;
                //}
            }
            return null;
        }
    }
}
