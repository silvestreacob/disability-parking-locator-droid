using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using dpark.Models;
using dpark.ViewModels.Base;

namespace dpark.ViewModels.MapSearch
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {

        }

        async public void LoadPin()
        {
            IsInitialized = false;
            IsBusy = true;
                      
            try
            {
                await AppData.Spaces.LoadMapData();

                //foreach(var item in AppData.Spaces.MapPinCollection)
                //{
                   
                //}

                IsInitialized = true;
                IsBusy = false;
            }
            catch (Exception)
            {
                IsInitialized = false;
            }
        }

        async private void DelayInit()
        {
            await Task.Delay(1000);
        }
    }
}
