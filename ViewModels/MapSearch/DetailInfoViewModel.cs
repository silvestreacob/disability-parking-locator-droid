using System;
using System.Diagnostics;

using dpark.Models;
using dpark.Models.Data;
using dpark.ViewModels.Base;
using dpark.Pages.MapSearch;

using Xamarin.Forms;

namespace dpark.ViewModels.MapSearch
{
    public class DetailInfoViewModel:BaseViewModel
    {
        public tmpSpaceData temp { get; set; }
        public DetailInfoViewModel(tmpSpaceData spaceData)
        {
            temp = spaceData;
        }
    }
}
