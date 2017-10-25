using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using dpark.Localization;
using dpark.Models.Data;
using dpark.Models;
using System.Threading;
using dpark.Models.WebService;
using System.Diagnostics;

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
        public static string VersionInfo;
        public static double logoHeight;
        public static double logoWidth;

        public static CancellationTokenSource CancellationToken { get; set; }
        private Label _label = new Label();
        private Client ServiceProvider { get; set; }

        public App()
		{
			InitializeComponent();

            app = this;

            //MainPage = new Pages.Splash.Splash();
            MainPage = new Pages.MapSearch.DetailPage();
        }

        public static void NewEventHandler(object sender, WebNavigatedEventArgs e)
        {
            Debug.WriteLine("[Forms WebView] {0}", e.Url.ToString());
        }

        protected override async void OnStart()
        {
            base.OnStart();
            if (AppData.Spaces.IsListDataUpdated == true)
            {
                GoToRoot();
                return;
            }

            await App.ExecuteIfConnected(async () =>
            {

                ServiceProvider = new Client();
                if (await ServiceProvider.GetSpaces())
                {
                    Debug.WriteLine("Finish loading data");

                    AppData.Spaces.IsListDataUpdated = true;
                    AppData.Spaces.IsDataUpdated = true;
                    GoToRoot();
                }

            });
        }
        
        protected override void OnSleep()
        {
            // Handle when your app sleeps
            if (App.CancellationToken != null)
            {
                App.CancellationToken.Cancel();
                //App.CancellationToken = null;
            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            if (AppData.Spaces.IsListDataUpdated == true)
                GoToRoot();
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
            }

            else
            {
                CurrentApp.MainPage = new Pages.RootTabAndroid();
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
                
            
                CurrentApp.MainPage = new Pages.Splash.Splash();
        }
        public static bool IsConnected
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }


        public static int AnimationSpeed = 250;
    }
}
