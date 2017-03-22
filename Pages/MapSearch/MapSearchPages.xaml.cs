using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

using static dpark.Statics.FontSizes;

using System.Diagnostics;

namespace dpark.Pages.MapSearch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapSearchPages : MapSearchPagesXaml
    {
        public MapSearchPages()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.300, -157.8167), Distance.FromMiles(5))); //Honolulu initial location            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await Navigation.PopAsync();
            
            if (AppData.Spaces.IsDataUpdated == false)
                return;

            AppData.Spaces.IsDataUpdated = false;
            await ViewModel.LoadPin(customMap);           
        }

        async public void OnSearch(object sender, EventArgs e)
        {
            ViewModel.IsBusy = true;
            var result = await ViewModel.OnButtonSearched(customMap, SearchFor.Text);

            if (result == "Not found")
            {
                await DisplayAlert("Not Found?", "Please ensure the address is typed correctly. e.g / i.e 919 Ala Moana Blvd", "OK");
                SearchFor.Focus();
            }

            else if (result == "No space nearby")
            {
                await DisplayAlert("Space nearby?", "We found no available parking space within the 10 mil radius.", "OK");
            }

            await Task.Delay(5000);
            ViewModel.IsBusy = false;
        }
    }

    public abstract class MapSearchPagesXaml : ModelBoundContentPage<MainViewModel> { }
}
