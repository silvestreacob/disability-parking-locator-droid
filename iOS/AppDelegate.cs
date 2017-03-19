using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using UserNotifications;

namespace dpark.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Forms.Init();

            Xamarin.FormsMaps.Init();
            App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height;

            LoadApplication(new App());            

            return base.FinishedLaunching(app, options);
		}
	}
}
