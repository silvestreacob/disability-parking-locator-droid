using System;
using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Pages.FlagSpace;
using dpark.Localization;
using Xamarin.Forms;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;

namespace dpark.Pages.MapSearch
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : DetailPageXaml
    {
        public DetailPage()
        {
            InitializeComponent();           
        }
        async void OnBackButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        async void GetDirectionTapped(object sender, EventArgs e)
        {
            if (await DisplayAlert(
                                TextResources.Leave_Application,
                                TextResources.Leave_MappingDirections,
                                TextResources.Leave_Mapping_Yes,
                                TextResources.Cancel))
            {

                var pin = await ViewModel.GetPin();

                await CrossExternalMaps.Current.NavigateTo(pin.Label, pin.Position.Latitude, pin.Position.Longitude, NavigationType.Driving);

                await ViewModel.PopModalAsync();
            }
        }

        async void ShowMapTapped(object sender, EventArgs e)
        {
            await ViewModel.PopModalAsync();

            var detailMapPage = new DetailMapPage()
            {
                BindingContext = new DetailInfoViewModel(ViewModel.temp)
            };
            await Navigation.PushModalAsync(detailMapPage);
        }

        async void FlagSpaceTapped(object sender, EventArgs e)
        {
            await ViewModel.PopModalAsync();

            var flagSpace = new FlagSpacePage();
            await Navigation.PushModalAsync(flagSpace);
        }      
    }

    public abstract class DetailPageXaml : ModelBoundContentPage<DetailInfoViewModel> { }
}
