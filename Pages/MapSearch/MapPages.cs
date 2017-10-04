using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;
using dpark.Models;
namespace dpark.Pages.MapSearch
{
    public class MapPages : ModelBoundContentPage<MainViewModel>
    {
        Map map;
    
        public MapPages()
        {
            map = new Map()
            {
                MapType = MapType.Street,
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
        }

        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (AppData.Spaces.IsDataUpdated == false)
        //        return;

        //    AppData.Spaces.IsDataUpdated = false;
        //   // await ViewModel.LoadPin(map);
        //}

    }
}