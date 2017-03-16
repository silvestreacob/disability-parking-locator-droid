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
            if (Device.OS == TargetPlatform.iOS && Device.Idiom == TargetIdiom.Phone)
            {
                //CurrentApp.MainPage.DisplayAlert("Completed","Load completed","OK");
                CurrentApp.MainPage = new Pages.MapSearch.SampleMapPage();
            }

            //IOS Tablet design
            else if (Device.OS == TargetPlatform.iOS && Device.Idiom == TargetIdiom.Tablet)
            {

            }

            //Android phones
            else if (Device.OS == TargetPlatform.Android && Device.Idiom == TargetIdiom.Phone)
            {

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
            var alert = await CurrentApp.MainPage.DisplayAlert(
                TextResources.NetworkConnection_Alert_Title,
                TextResources.NetworkConnection_Alert_Message,
                TextResources.Cancel,
                TextResources.NetworkConnection_Alert_TryAgain);

            if (alert == true) //if Try Again
            {
                CurrentApp.MainPage = new Pages.Splash.SplashPage();
            }
        }
        public static bool IsConnected
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        public static int AnimationSpeed = 250;
    }
}
