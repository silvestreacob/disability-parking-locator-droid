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
            LoadPin();

            var stack = new StackLayout { Spacing = 0 };
            var searchStack = new StackLayout { Opacity = 0.8, Padding = new Thickness(0, 0, 0, 0) };

            var searchAddress = new SearchBar { Placeholder = "Search for place or address", BackgroundColor = Xamarin.Forms.Color.White };
            searchStack.Children.Add(searchAddress);

            searchAddress.SearchButtonPressed += async (e, a) => {
                //LoadPin();
                var result = await AppData.Spaces.GeocodeAddress(searchAddress.Text);

                if (result == "")
                {
                    Debug.WriteLine("No Identifier found for pin");
                    return;
                }

                string[] index = result.Split('&');

                var address = index[0];
                var lat = Convert.ToDouble(index[1]);
                var lon = Convert.ToDouble(index[2]);
                var name = index[3];

                var position = new Position(lat, lon);
                customMap.Pins.Add(new Pin
                {
                    Label = "Search Location",
                    Position = position,
                    Address = address
                });

                Debug.WriteLine(customMap.Pins.Count);
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(2)));
            };

            customMap = new CustomMap()
            {
                IsShowingUser = true,  
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.300, -157.8167), Distance.FromMiles(5)));

            stack.Children.Add(searchStack);
            stack.Children.Add(customMap);
                      
            Content = stack;         

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();            
        }
      
        async private void LoadPin()
        {
            await Task.Delay(4000);
            customMap.CustomPins = new List<CustomPin>();

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

                Debug.WriteLine(pin.Id.ToString());

                customMap.CustomPins.Add(pin);               
                customMap.Pins.Add(pin.Pin);
            }
        }
    }
}
