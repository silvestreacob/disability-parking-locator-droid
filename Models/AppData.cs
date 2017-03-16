using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dpark.Models.Data;
using dpark.Models.WebService;

namespace dpark.Models
{
    static class AppData
    {
        public class SpaceContainer
        {
            public SpaceContainer()
            {
                _posts = new List<SpaceData>();
                _mapPinCollection = new ObservableCollection<MapPinData>();
            }

            #region Post Collection
            private List<SpaceData> _posts;
            public List<SpaceData> PostsCollection
            {
                get { return _posts; }
            }
            #endregion

            #region MapPinCollection
            private ObservableCollection<MapPinData> _mapPinCollection;
            public ObservableCollection<MapPinData> MapPinCollection
            {
                get { return _mapPinCollection; }
            }
            #endregion

            private Client ServiceProvider { get; set; }

            //to SplashViewModel
            async public Task<bool> LoadSpaces()
            {
                bool isSuccess = false;

                try
                {
                    _posts.Clear();
                    ServiceProvider = new Client();
                    isSuccess = await ServiceProvider.GetSpaces();

                }
                catch (Exception)
                {
                    isSuccess = false;
                }
                
                return isSuccess;
            }

            //to MainMapPage ViewModel
            async public Task<bool> LoadMapData()
            {
                bool isSuccess = false;

                try
                {
                    AppData.Spaces.MapPinCollection.Clear();

                    foreach (var item in PostsCollection)
                    {
                        MapPinData mapPinData = new MapPinData();
                        mapPinData.Title = item.Title;
                        mapPinData.StreetAddress = item.StreetAddress;
                        mapPinData.GeoLatitude = item.GeoLatitude;
                        mapPinData.GeoLongitude = item.GeoLongitude;
                        mapPinData.ImageURL = item.ImageURL;

                        AppData.Spaces.MapPinCollection.Add(mapPinData);
                    }

                    await Task.Delay(1000);
                    isSuccess = true;
                }

                catch (Exception)
                {
                    isSuccess = false;
                }

                return isSuccess;
            }

        }

        static public SpaceContainer Spaces = new SpaceContainer();
    }
}
