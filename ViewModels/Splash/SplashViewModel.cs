﻿using System;
using System.Diagnostics;
using Xamarin.Forms;
using System.Threading.Tasks;
using dpark.ViewModels.Base;
using dpark.Models;
using dpark.Models.WebService;

namespace dpark.ViewModels.Splash
{
    public class SplashViewModel : BaseViewModel
    {
        bool IsBusyLoading;
        /// <summary>
        /// Gets or sets the "IsBusy" property
        /// </summary>
        /// <value>The isbusy property.</value>
        public const string isBusyPropertyName = "IsBusyLoading";

        public bool isBusy
        {
            get { return IsBusyLoading; }
            set { SetProperty(ref IsBusyLoading, value, IsBusyPropertyName); }
        }
        //public async Task<bool> IsLoadSpaceData()
        //{
        //    IsBusy = true;
        //    bool isSuccess = false;

        //    try
        //    {
        //        isSuccess = await AppData.Spaces.LoadSpaces();

        //    }
        //    catch (Exception)
        //    {
        //        isSuccess = false;
        //    }

        //    IsBusy = false;
        //    return isSuccess;           
        //}

        //async private void DelayInit()
        //{
        //    await Task.Delay(1000);
        //}

    }
}
