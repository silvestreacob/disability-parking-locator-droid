using System.Threading.Tasks;
using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Localization;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;

namespace dpark.Pages.MapSearch
{
    public class DetailMapPage : ModelBoundContentPage<DetailInfoViewModel>
    {
        Map map;
        public DetailMapPage()
        {
            map = new Map()
            {
                MapType = MapType.Street,
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            //ToolbarItems.Add(
            //    new ToolbarItem(
            //        TextResources.Get_Directions,
            //        null,
            //        async () =>
            //        {
            //            if (await DisplayAlert(
            //                    TextResources.Leave_Application,
            //                    TextResources.Leave_MappingDirections,
            //                    TextResources.Leave_Mapping_Yes,
            //                    TextResources.Cancel))
            //            {

            //                var pin = await ViewModel.GetPin();

            //                await CrossExternalMaps.Current.NavigateTo(pin.Label, pin.Position.Latitude, pin.Position.Longitude, NavigationType.Driving);

            //                await ViewModel.PopAsync();
            //            }
            //        }
            //    )
            //);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await MakeMap();
        }
        public async Task MakeMap()
        {
            Task<Pin> getPinTask = ViewModel.GetPin();

            Pin pin = await getPinTask;

            map.Pins.Clear();
            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: map,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            Content = relativeLayout;
        }
    }
}
