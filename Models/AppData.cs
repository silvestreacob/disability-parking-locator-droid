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

            
            async public Task<string> GeocodeAddress(string address)
            {
                try
                {
                    ServiceProvider = new Client();
                    var results = await ServiceProvider.GeocodeEnteredAddress(address);
                    return results;
                }
                catch { return ""; }
            }            

        }

        static public SpaceContainer Spaces = new SpaceContainer();
    }
}
