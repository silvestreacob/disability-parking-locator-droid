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
        public Client ServiceProvider { get; set; }

        public App()
		{
			InitializeComponent();

            app = this;

            MainPage = new Pages.Splash.Splash();
            //MainPage = new Pages.MapSearch.DetailPage();
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
            if (App.CancellationToken != null)
            {
                App.CancellationToken.Cancel();
            }
        }

        protected override void OnResume()
        {
            if (AppData.Spaces.IsListDataUpdated == true)
                GoToRoot();
        }

        public static void GoToRoot()
        {
            if (Device.RuntimePlatform == Device.Android && Device.Idiom == TargetIdiom.Phone)
            {
                CurrentApp.MainPage = new Pages.RootPage();               
            }

            else
            {
                CurrentApp.MainPage = new Pages.RootPage();
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
