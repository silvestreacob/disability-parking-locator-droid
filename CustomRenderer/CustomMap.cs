using System.Collections.Generic;
using Xamarin.Forms.Maps;
using dpark.Models.Data;
using dpark.Models;
using dpark.Pages.MapSearch;
using dpark.ViewModels.MapSearch;

namespace dpark.CustomRenderer
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

        public string tmpID;
        async public void ShowPinDetailInfo(string id)
        {
            await Navigation.PopToRootAsync();
            tmpSpaceData detailInfo = null;

            foreach (var item in AppData.Spaces.tmpSpaceCollection)
                if (item.ID == id)
                {
                    detailInfo = item;
                    break;
                }

            if (detailInfo == null)
                return;

            var detailInfoPage = new DetailPage()
            {
                BindingContext = new DetailInfoViewModel(detailInfo)
            };
            await Navigation.PushModalAsync(detailInfoPage, true);

        }
    }
}
