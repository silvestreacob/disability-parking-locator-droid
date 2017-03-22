using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using dpark.Models;
using dpark.Models.Data;
using dpark.ViewModels.Base;
using dpark.Pages.MapSearch;
using dpark.CustomRenderer;
using dpark.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace dpark.ViewModels.MapSearch
{
    public class MainViewModel : BaseViewModel
    {
        #region SpaceData
        public SpaceData SpaceData { get; set; }
        #endregion

        readonly Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
        }

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

        async public Task <string> OnButtonSearched(CustomMap customMap, string searchText)
        {
            var result = await AppData.Spaces.GeocodeAddress(searchText);
            if (result == "")
            {
                IsBusy = false;
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
                    tmpSpaceData tmp = new tmpSpaceData(item, value);
                    AppData.Spaces.tmpSpaceCollection.Add(tmp);
                    Debug.WriteLine(item.ThumbnailImageUrl + "\n");
                }
            }

            if (AppData.Spaces.tmpSpaceCollection.Count == 0)
            {
                return "No space nearby";
            }

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

            AppData.Spaces.IsListDataUpdated = true;
            return "Found";
        }        

    }
}
