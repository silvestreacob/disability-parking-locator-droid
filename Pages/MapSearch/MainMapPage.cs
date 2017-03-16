using Xamarin.Forms;
using Xamarin.Forms.Maps;
using dpark.Pages.Base;
using dpark.Models;
using dpark.ViewModels.MapSearch;
using dpark.CustomRenderer;

namespace dpark.Pages.MapSearch
{
    public class MainMapPage : ModelBoundContentPage<MainViewModel>
    {
        public CustomMap customMap;
        public MainMapPage()
        {
            customMap = new CustomMap()
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(5)));

            foreach(var item in AppData.Spaces.MapPinCollection)
            {
                customMap.PinList.Add(item);
            }

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized == true)
                return;

            ViewModel.LoadPin();
        }
    }
}
