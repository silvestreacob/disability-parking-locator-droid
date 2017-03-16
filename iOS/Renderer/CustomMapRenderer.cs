using dpark.CustomRenderer;
using dpark.iOS.Renderer;
using dpark.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

using MapKit;
using UIKit;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace dpark.iOS.Renderer
{
    public class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        List<MapPinData> customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
                //nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.PinList;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                //nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
            }
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

            if (annotationView == null)
            {
                annotationView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(customPin.Title);

                if (annotationView == null)
                    annotationView = new MKPinAnnotationView(anno, customPin.Title);

                ((MKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
                ((MKPinAnnotationView)annotationView).AnimatesDrop = true;
                annotationView.CanShowCallout = true;
            }

            return annotationView;
        }

        MapPinData GetCustomPin(MKPointAnnotation annotation)
        {
            try
            {
                foreach (var item in customPins)
                {
                    var position = new Position(item.GeoLatitude, item.GeoLongitude);
                    return item;
                }
               
            }
            catch { }
            return null;
        }
    }
}
