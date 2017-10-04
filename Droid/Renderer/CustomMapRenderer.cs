using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using dpark.CustomRenderer;
using dpark.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using Android.Util;
using Java.IO;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace dpark.Droid.Renderer
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        List<CustomPin> customPins;
        bool isDrawn;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                //customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);                            
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            //System.Console.WriteLine("THis is customPins count: " + customPins.Count);

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                var formsMap = (CustomMap)Element;
                NativeMap.Clear();
                NativeMap.InfoWindowClick += OnInfoWindowClick;
                NativeMap.SetInfoWindowAdapter(this);

                foreach (var pin in formsMap.CustomPins)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                    marker.SetTitle(pin.Pin.Label);
                    marker.SetSnippet(pin.Pin.Address);
                    //marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));

                    NativeMap.AddMarker(marker);
                }
                isDrawn = true;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (changed)
            {
                isDrawn = false;
            }
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                return;
               // throw new Exception("Custom pin not found");
            }

            var formsMap = (CustomMap)Element;
            if (!string.IsNullOrWhiteSpace(customPin.Id))
            {
                //var url = Android.Net.Uri.Parse(customPin.Url);
                //var intent = new Intent(Intent.ActionView, url);
                //intent.AddFlags(ActivityFlags.NewTask);
                //Android.App.Application.Context.StartActivity(intent);
                formsMap.ShowPinDetailInfo(customPin.Id);
            }
        }
        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                //if (customPin == null)
                //{
                //    throw new Exception("Custom pin not found");
                //}

                //if (customPin.Id == "Xamarin")
                //{
                //    view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
                //}
                //else
                //{
                //    view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
                //}
                view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            var formsMap = (CustomMap)Element;

            foreach (var pin in formsMap.CustomPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}