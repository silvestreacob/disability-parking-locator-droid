using Xamarin.Forms;

namespace dpark.Pages.Submit
{
    public class WebSubmitPage : ContentPage
    {
        WebView webView;
        public WebSubmitPage()
        {
            var layout = new StackLayout();
            webView = new WebView() { HeightRequest = 1000, WidthRequest = 1000, Source = "https://dpark.us/add" };
            layout.Children.Add(webView);
            Content = layout;
        }
    }
}
