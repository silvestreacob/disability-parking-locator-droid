using Xamarin.Forms;

namespace dpark.Pages.FlagSpace
{
    public class FlagSpacePage : ContentPage
    {
        WebView webView;
        public FlagSpacePage()
        {
            var layout = new StackLayout();
            webView = new WebView() { HeightRequest = 1000, WidthRequest = 1000, Source = "https://dpark.us/simple-flag" };
            layout.Children.Add(webView);
            Content = layout;
        }
    }
}
