using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using dpark.Localization;
using dpark.Models.Data;

namespace dpark
{
	public partial class App : Application
	{
        static Application app;
        public static Application CurrentApp
        {
            get { return app; }
        }

        public static double ScreenHeight;
        public static double ScreenWidth;
        public App()
		{
			InitializeComponent();

            app = this;
            MainPage = new Pages.Splash.SplashPage();
        }

        public static void GoToRoot()
        {
            //Target IOS Phone
            if (Device.RuntimePlatform == Device.iOS && Device.Idiom == TargetIdiom.Phone)
            {
                CurrentApp.MainPage = new Pages.RootTabPage();
            }

            //IOS Tablet design
            else if (Device.RuntimePlatform == Device.iOS && Device.Idiom == TargetIdiom.Tablet)
            {
                CurrentApp.MainPage = new Pages.RootTabPage();
            }

            //Android phones
            else if (Device.RuntimePlatform == Device.Android && Device.Idiom == TargetIdiom.Phone)
            {
                CurrentApp.MainPage = new Pages.RootTabAndroid();
                //CurrentApp.MainPage = new Pages.MapSearch.MapSearchPages();
                //CurrentApp.MainPage = new Pages.List.ListPage()
                //{
                //    //Title = "Results List",
                //    Icon = new FileImageSource { File = "list.png" },
                //    BindingContext = new ViewModels.List.ListViewModel()
                //};
                //CurrentApp.MainPage = new Pages.Submit.WebSubmitPage();
                //CurrentApp.MainPage = new Pages.About.AboutPage();
                //CurrentApp.MainPage = new TabbedPage
                //{
                //    Title = "Disability Parking Locator",
                //    Icon = new FileImageSource { File = "icon.png" },
                //    Children =
                //        {
                //            new NavigationPage(new Pages.MapSearch.MapSearchPages())
                //            {
                //                 //Title = "Map",
                //                 Icon = new FileImageSource { File = "mapsearch.png" },
                //                 BindingContext = new ViewModels.MapSearch.MainViewModel()
                //            },
                //            new NavigationPage(new Pages.List.ListPage())
                //            {
                //                //Title = "Results List",
                //                Icon = new FileImageSource { File = "list.png" },
                //                BindingContext = new ViewModels.List.ListViewModel()
                //            },
                //            new NavigationPage(new Pages.Submit.WebSubmitPage())
                //            {
                //                //Title = "Submit",
                //                Icon = new FileImageSource { File = "submit.png" }
                //            },
                //            new NavigationPage(new Pages.About.AboutPage())
                //            {
                //                //Title = "About",
                //                Icon = new FileImageSource { File = "about.png" },
                //                BindingContext = new ViewModels.About.AboutViewModel()
                //            },
                //        }
                //};
            }

            else
            {

            }
        }

        public static async Task ExecuteIfConnected(Func<Task> actionToExecuteIfConnected)
        {
            if (IsConnected)
            {
                await actionToExecuteIfConnected();
            }
            else
            {
                await ShowNetworkConnectionAlert();
            }
        }
        static async Task ShowNetworkConnectionAlert()
        {
            await CurrentApp.MainPage.DisplayAlert(
                TextResources.NetworkConnection_Alert_Title,
                TextResources.NetworkConnection_Alert_Message,
                TextResources.NetworkConnection_Alert_TryAgain);

                CurrentApp.MainPage = new Pages.Splash.SplashPage();
        }
        public static bool IsConnected
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        public static int AnimationSpeed = 250;
    }
}
