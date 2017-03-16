using System;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using dpark.Models;
using dpark.ViewModels.Base;
using System.Collections.Generic;
using dpark.Models.Data;

namespace dpark.ViewModels.MapSearch
{
    public class MainViewModel : BaseViewModel
    {
       
        public MainViewModel()
        {

        }

        async public void LoadPin()
        {
            Debug.WriteLine(AppData.Spaces.MapPinCollection.Count);

            //IsInitialized = false;
            //IsBusy = true;
                      
            //try
            //{
            //    await AppData.Spaces.LoadMapData();

            //    IsInitialized = true;
            //    IsBusy = false;
            //}
            //catch (Exception)
            //{
            //    IsInitialized = false;
            //}
        }

        async private void DelayInit()
        {
            await Task.Delay(1000);
        }
    }
}
