using System;
using System.Diagnostics;

using dpark.Models;
using dpark.Models.Data;
using dpark.ViewModels.Base;
using dpark.Pages.MapSearch;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

namespace dpark.ViewModels.MapSearch
{
    public class DetailInfoViewModel:BaseViewModel
    {
        public tmpSpaceData temp { get; set; }
        public DetailInfoViewModel(tmpSpaceData spaceData)
        {
            temp = spaceData;
        }
        public async Task<Pin> GetPin()
        {
            await Task.Delay(0);         
            Position p = new Position(temp.Latitude, temp.Longitude);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = p,
                Label = temp.Title,
                Address = temp.StreetAddress
            };

            return pin;
        }
    }
}
