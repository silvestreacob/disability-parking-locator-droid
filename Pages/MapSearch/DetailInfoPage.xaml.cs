using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dpark.Models.Data;
using dpark.Models;

namespace dpark.Pages.MapSearch
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailInfoPage : ContentPage
    {
        public SpaceData pinData { get; set; }
        readonly Page _CurrentPage;
        public Page CurrentPage { get { return _CurrentPage; } }

        public DetailInfoPage(SpaceData pinData)
        {
            InitializeComponent();
            Title = pinData.Title;
           // _CurrentPage = currentPage;

            Debug.WriteLine(AppData.Spaces.MapPinCollection.Count + "\n" + Title);
        }
    }
}
