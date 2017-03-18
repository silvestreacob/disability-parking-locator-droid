using System;
using System.Diagnostics;

using dpark.Models;
using dpark.Models.Data;
using dpark.ViewModels.Base;
using dpark.Pages.MapSearch;

namespace dpark.ViewModels.MapSearch
{
    public class DetailInfoViewModel:BaseViewModel
    {
        public DetailInfoViewModel(SpaceData spaceData)
        {
            this.Title = spaceData.Title;
            Debug.WriteLine(Title);
        }
    }
}
