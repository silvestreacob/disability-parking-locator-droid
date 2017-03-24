using System.Threading.Tasks;
using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Localization;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using System;

namespace dpark.Pages.MapSearch
{
    public class DetailMapPage : ModelBoundContentPage<DetailInfoViewModel>
    {
        Map map;
        Button backButton;
        public DetailMapPage()
        {
            map = new Map()
            {
                MapType = MapType.Street,
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            backButton = new Button
            {
                Text = "Back",
                TextColor = Color.White,
                Image = "back_ios.png"
            };
            backButton.Clicked += OnBackButtonClicked;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await MakeMap();
        }
        async void OnBackButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
        public async Task MakeMap()
        {
            Task<Pin> getPinTask = ViewModel.GetPin();

            Pin pin = await getPinTask;

            map.Pins.Clear();
            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));

            //RelativeLayout relativeLayout = new RelativeLayout();

            //relativeLayout.Children.Add(
            //    view: map,
            //    widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
            //    heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            //);

            StackLayout stackLayout = new StackLayout();
            Content = stackLayout;
        }
    }
}
