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
    public partial class DetailPage : DetailPageXaml
    {
        public DetailPage()
        {
            InitializeComponent();
            SetToolBarClose();
        }

        void SetToolBarClose()
        {
            ToolbarItems.Clear();
            ToolbarItems.Add(GetCloseToolBarItem());
            
        }

        ToolbarItem GetCloseToolBarItem()
        {
            ToolbarItem closeToolBarItem = new ToolbarItem();
            closeToolBarItem.Text = "Close";
            closeToolBarItem.Icon = "close.png";
            closeToolBarItem.Clicked += CloseToolBarItem_Clicked;
            return closeToolBarItem;
        }

        async void CloseToolBarItem_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
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
            }
        }

        async void ShowMapTapped(object sender, EventArgs e)
        {

            var detailMapPage = new DetailMapPage()
            {
                BindingContext = new DetailInfoViewModel(ViewModel.temp)
            };
            
            await Navigation.PushAsync(detailMapPage);

        }

        async void FlagSpaceTapped(object sender, EventArgs e)
        {

            var flagSpace = new FlagSpacePage();
            await Navigation.PushAsync(flagSpace);
        }
    }

    public abstract class DetailPageXaml : ModelBoundContentPage<DetailInfoViewModel> { }
}
