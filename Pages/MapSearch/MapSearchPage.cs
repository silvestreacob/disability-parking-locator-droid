using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Models;
using dpark.Models.Data;
using dpark.CustomRenderer;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace dpark.Pages.MapSearch
{
    public class MapSearchPage : ModelBoundContentPage<MainViewModel>
    {
        public MapSearchPage()
        {
            BindingContext = new MainViewModel();
            ViewModel.IsInitialized = false;
            ViewModel.IsBusy = true;

            var stack = new StackLayout { Spacing = 0 };
            var searchStack = new StackLayout { Opacity = 0.8, Padding = new Thickness(0, 0, 0, 0) };

            CustomMap customMap = new CustomMap()
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.300, -157.8167), Distance.FromMiles(5))); //Honolulu initial location

            var searchAddress = new SearchBar { Placeholder = "Search for place or address", BackgroundColor = Xamarin.Forms.Color.White };
            searchAddress.SearchButtonPressed += async (e, a) => {
                await ViewModel.OnButtonSearched(customMap, searchAddress.Text);
            };
            searchStack.Children.Add(searchAddress);            

            stack.Children.Add(searchStack);
            stack.Children.Add(customMap);

            Content = stack;

            ViewModel.LoadPin(customMap);
            ViewModel.IsBusy = false;
        }

        
    }
}
