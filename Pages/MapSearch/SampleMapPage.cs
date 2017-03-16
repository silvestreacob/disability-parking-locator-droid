using dpark.CustomRenderer;
using dpark.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace dpark.Pages.MapSearch
{
    public class SampleMapPage : ContentPage
    {
        CustomMap customMap;
        public SampleMapPage()
        {
            customMap = new CustomMap()
            {
                IsShowingUser = true,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.300, -157.8167), Distance.FromMiles(5)));

            Button button = new Button
            {
                Text = "Click Me!",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnButtonClicked;

            Content = customMap;

            //LoadPin();
            Task.Delay(1000);

            foreach (var item in AppData.Spaces.PostsCollection)
            {
                var pin = new CustomPin
                {
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(item.GeoLatitude, item.GeoLongitude),
                        Label = item.Title,
                        Address = item.StreetAddress
                    },

                    Id = item.ID,
                    Url = item.ImageURL
                };

                customMap.CustomPins = new List<CustomPin> { pin };
                customMap.Pins.Add(pin.Pin);
            }

            Task.Delay(6000);
            //AnimateLoadPin();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        async private void AnimateLoadPin()
        {
            //customMap.Pins.Clear();
                      
            foreach (var item in customMap.CustomPins)
            {
                customMap.CustomPins = new List<CustomPin> { item };
                customMap.Pins.Add(item.Pin);
            }           
        }
        async private void LoadPin()
        {
            await Task.Delay(1000);
            customMap.Pins.Clear();

            foreach (var item in AppData.Spaces.PostsCollection)
            {
                var pin = new CustomPin
                {
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(item.GeoLatitude, item.GeoLongitude),
                        Label = item.Title,
                        Address = item.StreetAddress
                    },

                    Id = item.ID,
                    Url = item.ImageURL
                };

                customMap.CustomPins = new List<CustomPin> { pin };
                customMap.Pins.Add(pin.Pin);
            }
        }

        public void OnButtonClicked(object sender, EventArgs args)
        {
            LoadPin();
        }
    }
}
