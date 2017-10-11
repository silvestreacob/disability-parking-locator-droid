using System;
using System.Diagnostics;

using Android.Content;
using Android.OS;
using Android.Webkit;
using Android.Provider;

using Java.IO;
using Java.Text;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using dpark.Droid.Renderer;

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(WebviewRenderer))]
namespace dpark.Droid.Renderer
{
    public class WebviewRenderer : WebViewRenderer
    {
        Xamarin.Forms.WebView formsWebView;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            var activity = Xamarin.Forms.Forms.Context as MainActivity;
            if (e.NewElement != null)
            {
                formsWebView = e.NewElement;
                Control.Settings.JavaScriptEnabled = true;
                Control.SetWebViewClient(new CustomWebViewClient(formsWebView));
                Control.SetWebChromeClient(new CustomWebChromeClient(activity));
            }
        }
    }

    public class CustomWebChromeClient : WebChromeClient
    {

        MainActivity WebViewActivity;
        private static int FILECHOOSER_RESULTCODE = 1;
        public CustomWebChromeClient(MainActivity activity)
        {
            WebViewActivity = activity;

        }

        //File chooser Android 5.0 >
        public override bool OnShowFileChooser(Android.Webkit.WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
           
            WebViewActivity = Xamarin.Forms.Forms.Context as MainActivity;
            WebViewActivity.mUploadMessage = filePathCallback;

            Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);

            //add temp file path later
            if (WebViewActivity.IsThereAnAppToTakePictures())
            {
                File photoFile = null;
                try
                {
                    photoFile = createImageFile();
                    takePictureIntent.PutExtra("PhotoPath", WebViewActivity.mCameraPhotoPath);
                }

                catch (Exception e)
                { System.Diagnostics.Debug.WriteLine("Unable to create Image File! " + e); }

                if (photoFile != null)
                {
                    WebViewActivity.mCameraPhotoPath = "file:" + photoFile.AbsolutePath;
                    takePictureIntent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(photoFile));
                }
                else { takePictureIntent = null; }
            }

            //gallery intent
            Intent galleryIntent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);

            Intent[] intentArray;
            if (takePictureIntent != null) { intentArray = new Intent[] { takePictureIntent }; }
            else { intentArray = new Intent[0]; }

            Intent i = new Intent(Intent.ActionChooser);
            i.PutExtra(Intent.ExtraIntent, galleryIntent);
            i.PutExtra(Intent.ExtraTitle, "Choose Image");
            i.PutExtra(Intent.ExtraInitialIntents, intentArray);

            WebViewActivity.StartActivityForResult(Intent.CreateChooser(i, "File Chooser"), FILECHOOSER_RESULTCODE);

            return true;
        }
               
        private File createImageFile()
        {
            string timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Java.Util.Date());
            string imageFileName = "JPEG_" + timeStamp + "_";
            File storageDir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
            File imageFile = File.CreateTempFile(imageFileName, ".jpg", storageDir);
                       
            return imageFile;
        }
        
    }
    public class CustomWebViewClient : WebViewClient
    {
        Xamarin.Forms.WebView formsWebView;
        public event EventHandler<WebNavigatedEventArgs> Navigated;

        public CustomWebViewClient(Xamarin.Forms.WebView webView)
        {
            formsWebView = webView;
            Navigated += App.NewEventHandler;
        }

        public override WebResourceResponse ShouldInterceptRequest(Android.Webkit.WebView view, IWebResourceRequest request)
        {
            System.Console.WriteLine("[Custom Delegate] Url: {0}", request.Url);

            return base.ShouldInterceptRequest(view, request);
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            var source = new UrlWebViewSource { Url = url };
            var args = new WebNavigatedEventArgs(WebNavigationEvent.NewPage, source, url, WebNavigationResult.Success);

            Navigated(formsWebView, args);
            base.OnPageFinished(view, url);
        }
    }
}