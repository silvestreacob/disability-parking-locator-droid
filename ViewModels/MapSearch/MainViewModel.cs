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

        }
                
        async private void DelayInit()
        {
            await Task.Delay(1000);
        }

        async public void LoadPin(CustomMap customMap)
        {
            if(IsInitialized == false)
                await Task.Delay(4000);

            IsInitialized = true;
            customMap.CustomPins = new List<CustomPin>();

            customMap.Pins.Clear();
            foreach (var item in AppData.Spaces.tmpSpaceCollection)
            {
                //if (item.Title == "Office Max Ala Moana 1") //testing
                //{
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
                //}
            }
        }

        async public Task OnButtonSearched(CustomMap customMap, string searchText)
        {
            var result = await AppData.Spaces.GeocodeAddress(searchText);

            if (result == "Hawaii, USA")
            {
                Debug.WriteLine("No Identifier found for pin");
                return;
            }

            //LoadPin(customMap); //load pins again

            string[] index = result.Split('&');

            var address = index[0];
            var lat = Convert.ToDouble(index[1]);
            var lon = Convert.ToDouble(index[2]);
            var name = index[3];

            //create new TempSorted SpaceData
            AppData.Spaces.tmpSpaceCollection.Clear();
            foreach (var item in AppData.Spaces.PostsCollection)
            {
                double value = GetDistance.DistanceFromMeToLocation(lat, lon, item.GeoLatitude, item.GeoLongitude);
                if(value <= 10) //within the 10 mi radius
                {
                    tmpSpaceData tmp = new tmpSpaceData(item, value);
                    AppData.Spaces.tmpSpaceCollection.Add(tmp);
                    Debug.WriteLine(item.Title + "\n");
                }
            }            

            LoadPin(customMap);

            var position = new Position(lat, lon);
            customMap.Pins.Add(new Pin
            {
                Label = "Search Location",
                Position = position,
                Address = address
            });
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.2)));
        }

        async public void ShowPinDetailInfo(string id)
        {
            SpaceData detailInfo = null;

            foreach (var item in AppData.Spaces.PostsCollection)
                if (item.ID == id)
                {
                    detailInfo = item;
                    break;
                }
#if DEBUG
            Debug.WriteLine(detailInfo.ID);
#endif
            if (detailInfo == null)
                return;

            var detailInfoPage = new DetailPage()
            {
                Title = detailInfo.Title
                //BindingContext = new DetailInfoViewModel(detailInfo)
            };
            await Navigation.PushAsync(detailInfoPage);

        }

       
    }
}
