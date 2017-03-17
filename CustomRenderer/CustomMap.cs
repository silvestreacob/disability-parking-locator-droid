using System.Collections.Generic;
using Xamarin.Forms.Maps;
using dpark.Models.Data;
using dpark.Models;
using dpark.Pages.MapSearch;
using System.Diagnostics;

namespace dpark.CustomRenderer
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

        async public void ShowPinDetailInfo(string id)
        {
            SpaceData detailInfo = null;

            foreach (var item in AppData.Spaces.PostsCollection)
                if(item.ID == id)
                {
                    detailInfo = item;
                    break;
                }

            if (detailInfo == null)
                return;

            var detailInfoPage = new DetailInfoPage(detailInfo)
            {
                //BindingContext = 
            };

            await Navigation.PushAsync(detailInfoPage);
                      
        }
    }
}
