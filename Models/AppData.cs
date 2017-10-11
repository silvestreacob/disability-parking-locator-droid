using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using dpark.Models.Data;
using dpark.Models.WebService;
using System.Threading;
using System.Diagnostics;
using Xamarin.Forms;

namespace dpark.Models
{
    static class AppData
    {
        public class SpaceContainer
        {
            public SpaceContainer()
            {
                _posts = new List<SpaceData>();
                _tmpSpaceCollection = new ObservableCollection<tmpSpaceData>();
            }

            #region Post Collection
            public List<SpaceData> _posts;
            public List<SpaceData> PostsCollection
            {
                get { return _posts; }
            }
            #endregion

            #region MapPinCollection
            private ObservableCollection<tmpSpaceData> _tmpSpaceCollection;
            public ObservableCollection<tmpSpaceData> tmpSpaceCollection
            {
                get { return _tmpSpaceCollection; }
            }
            #endregion

            private Client ServiceProvider { get; set; }

            public bool IsDataUpdated { get; set; }
            public bool IsListDataUpdated { get; set; }
            //to SplashViewModel
            private int _counter = 0;
            public async Task LoadInitialSpaces()
            {
                App.CancellationToken = new CancellationTokenSource();
                while (!App.CancellationToken.IsCancellationRequested)
                {
                    try
                    {
    
                        App.CancellationToken.Token.ThrowIfCancellationRequested();
                        await Task.Delay(1000, App.CancellationToken.Token).ContinueWith(async (arg) => {
                            if (!App.CancellationToken.Token.IsCancellationRequested)
                            {
                                App.CancellationToken.Token.ThrowIfCancellationRequested();

                                _posts.Clear();
                                ServiceProvider = new Client();
                                await ServiceProvider.GetSpaces();
                                App.GoToRoot();

                                Device.BeginInvokeOnMainThread(() => (++_counter).ToString());
                            }
                        });
                    }

                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
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
