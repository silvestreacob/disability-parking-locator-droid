using System;
using dpark.Localization;
using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace dpark.Pages.MapSearch
{

    public partial class MapSearchPages : MapSearchPagesXaml
    {
        public MapSearchPages()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();

            SetToolBarItems();
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.300, -157.8167), Distance.FromMiles(5))); //Honolulu initial location                         
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (AppData.Spaces.IsDataUpdated == false)
                return;

            AppData.Spaces.IsDataUpdated = false;
            await ViewModel.LoadPin(customMap);           
        }

        void SetToolBarItems()
        {
            ToolbarItems.Clear();
            ToolbarItems.Add(GetRefreshToolBarItem());
        }

        ToolbarItem GetRefreshToolBarItem()
        {
            ToolbarItem refreshToolBarItem = new ToolbarItem();
            refreshToolBarItem.Text = TextResources.Refresh_Space;
            refreshToolBarItem.Icon = "refresh.png";
            refreshToolBarItem.Clicked += RefreshToolBarItem_Clicked;
            return refreshToolBarItem;
        }

        void RefreshToolBarItem_Clicked(object sender, EventArgs e)
        {
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.300, -157.8167), Distance.FromMiles(5))); //Honolulu initial location 
            ViewModel.IsFirstime = false;    
            ViewModel.IsInitialized = false;
            ViewModel.RefreshPin(customMap);
        }

        async public void OnSearch(object sender, EventArgs e)
        {
            var result = await ViewModel.OnButtonSearched(customMap, SearchFor.Text);


            if (result == "Not found")
            {
                SearchFor.Focus();

                await DisplayAlert(TextResources.SearchNotFound_Title,
                    TextResources.SearchNotFound_Message,
                    TextResources.TryAgain);
            }

            else if (result == "No space nearby")
            {
                SearchFor.Focus();

                await DisplayAlert(TextResources.SearchSpaceNearby_Title,
                    TextResources.SearchSpaceNearby_Message,
                    TextResources.TryAgain);
            }
          
        }
    }

    public abstract class MapSearchPagesXaml : ModelBoundContentPage<MainViewModel> { }
}
