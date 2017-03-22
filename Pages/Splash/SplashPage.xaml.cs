using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dpark.Pages.Base;
using dpark.ViewModels.Splash;
using dpark.Models;

using Xamarin.Forms.Xaml;

namespace dpark.Pages.Splash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : SplashPageXaml
    {
        public SplashPage()
        {
            InitializeComponent();
            BindingContext = new SplashViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await AppLoading();
        }

        async Task AppLoading()
        {
            IsBusy = true;

            await App.ExecuteIfConnected(async () =>
            {
                //ViewModel.IsPresentingLoginUI = true;

                if (!await ViewModel.IsLoadSpaceData())
                {
                    await DisplayAlert("Loading data error", "An unknown error has occurred.Please try again.", "OK");
                    ViewModel.IsInitialized = false;
                    return;
                }

                AppData.Spaces.IsListDataUpdated = true;
                AppData.Spaces.IsDataUpdated = true;
                App.GoToRoot();
            });

            ViewModel.IsInitialized = true;
            IsBusy = false;
        }
    }
    
    public abstract class SplashPageXaml : ModelBoundContentPage<SplashViewModel> { }
}
