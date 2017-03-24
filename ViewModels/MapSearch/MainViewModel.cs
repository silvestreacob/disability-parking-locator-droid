using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using dpark.Models;
using dpark.Models.Data;
using dpark.ViewModels.Base;
using dpark.CustomRenderer;
using dpark.Helpers;

using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace dpark.ViewModels.MapSearch
{
    public class MainViewModel : BaseViewModel
    {
        #region SpaceData
        public SpaceData SpaceData { get; set; }
        #endregion

        public MainViewModel()
        {
            // _currentPage = ;
            //IsBusy = true;
        }
                
        async private void DelayInit()
        {
            await Task.Delay(1000);
        }

        async public Task LoadPin(CustomMap customMap)
        {
            IsBusy = true;
            if(IsInitialized == false)
                await Task.Delay(4000);

            IsInitialized = true;
            customMap.CustomPins = new List<CustomPin>();

            customMap.Pins.Clear();
            foreach (var item in AppData.Spaces.tmpSpaceCollection)
            {        
                    var pin = new CustomPin
                    {
                        Pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = new Position(item.Latitude, item.Longitude),
                            Label = item.Title,
                            Address = item.StreetAddress
                        },

                        Id = item.ID,
                        Url = item.ImageURL

                    };

                    customMap.CustomPins.Add(pin);
                    customMap.Pins.Add(pin.Pin);
            }

            IsBusy = false;
        }
        async public void RefreshPin(CustomMap customMap)
        {
            IsBusy = true;
            if (IsInitialized == false)
                await Task.Delay(0);

            IsInitialized = true;
            customMap.CustomPins = new List<CustomPin>();


            double lat, lon;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var p = await locator.GetPositionAsync(timeoutMilliseconds: 1000);
                lat = p.Latitude;
                lon = p.Longitude;
            }
            catch(Exception)
            {
                lat = 21.300;
                lon = -157.8167;
            }

            customMap.Pins.Clear();
            AppData.Spaces.tmpSpaceCollection.Clear();
            foreach (var item in AppData.Spaces.PostsCollection)
            {
                tmpSpaceData tmp = new tmpSpaceData(item, lat, lon);
                AppData.Spaces.tmpSpaceCollection.Add(tmp);

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

                customMap.CustomPins.Add(pin);
                customMap.Pins.Add(pin.Pin);
            }

            AppData.Spaces.IsListDataUpdated = true;
            IsBusy = false;
        }

        async public Task <string> OnButtonSearched(CustomMap customMap, string searchText)
        {
            IsSearching = true;
            var result = await AppData.Spaces.GeocodeAddress(searchText);
            System.Diagnostics.Debug.WriteLine(result);

            if (result == "")
            {
                IsSearching = false;
                return "Not found";
            }

            string[] index = result.Split('&');

            var address = index[0];
            var lat = Convert.ToDouble(index[1]);
            var lon = Convert.ToDouble(index[2]);
            var name = index[3];

            AppData.Spaces.tmpSpaceCollection.Clear();
            foreach (var item in AppData.Spaces.PostsCollection)
            {
                double value = GetDistance.DistanceFromMeToLocation(lat, lon, item.GeoLatitude, item.GeoLongitude);
                if(value <= 10) //within the 10 mi radius
                {
                    tmpSpaceData tmp = new tmpSpaceData(item, lat, lon);
                    AppData.Spaces.tmpSpaceCollection.Add(tmp);
                }
            }

            if (AppData.Spaces.tmpSpaceCollection.Count == 0)
            {
                IsSearching = false;
                return "No space nearby";
            }
            AppData.Spaces.IsListDataUpdated = true;
            IsFirstime = true;
            IsSearching = false;

            IsBusy = true;
            await LoadPin(customMap);

            await Task.Delay(1000);
            var position = new Position(lat, lon);
            customMap.Pins.Add(new Pin
            {
                Label = "Search Location",
                Position = position,
                Address = address
            });
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.2)));

            
            IsBusy = false;
            return "Found";
        }        

    }
}
