using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Webkit;
using Android.Provider;

using Uri = Android.Net.Uri;

using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ImageCircle.Forms.Plugin.Droid;

namespace dpark.Droid
{
    [Activity(Label = "Disability Parking Locator", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        public IValueCallback mUploadMessage;
        public Android.Webkit.WebView webview;
        private static int FILECHOOSER_RESULTCODE = 1;
        public string mCameraPhotoPath;
        public Uri mCapturedImageURI = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            ImageCircleRenderer.Init();

            FormsMaps.Init(this, bundle);
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;

            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            App.logoHeight = App.ScreenHeight / 6;
            App.logoWidth = App.ScreenWidth / 1.6;

            PackageManager manager = this.PackageManager;
            PackageInfo info = manager.GetPackageInfo(this.PackageName, 0);

            App.VersionInfo = info.VersionName;
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                if (requestCode != FILECHOOSER_RESULTCODE && mUploadMessage == null)
                {
                    base.OnActivityResult(requestCode, resultCode, data);
                    return;
                }

                Uri[] results = null;

                if (resultCode == Result.Ok)
                {
                    results = data == null || resultCode != Result.Ok ? null : new Android.Net.Uri[] { data.Data };

                    if (data == null)
                    {
                        if (mCameraPhotoPath != null)
                        {
                            results = new Uri[] { Uri.Parse(mCameraPhotoPath) };
                        }
                    }
                }

                mUploadMessage.OnReceiveValue(results);
                mUploadMessage = null;
            }

        }

        public bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
    }
}
