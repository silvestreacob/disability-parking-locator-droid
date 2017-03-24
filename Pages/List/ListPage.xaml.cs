using System;
using dpark.Pages.Base;
using dpark.ViewModels.List;
using dpark.Models;
using dpark.Models.Data;
using dpark.Pages.MapSearch;
using dpark.ViewModels.MapSearch;
using dpark.Localization;
using dpark.Pages.Submit;
using Xamarin.Forms;

namespace dpark.Pages.List
{    
    public partial class ListPage : ListPageXaml
    {
        public ListPage()
        {
            InitializeComponent();
            BindingContext = new ListViewModel();           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            if (AppData.Spaces.IsListDataUpdated == false)
                return;

            ViewModel.LoadSpaces.Execute(null);
            AppData.Spaces.IsListDataUpdated = false;
        }
      
        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            tmpSpaceData detailInfo = (tmpSpaceData)e.Item;

            var detailInfoPage = new DetailPage()
            {
                BindingContext = new DetailInfoViewModel(detailInfo) { Navigation = this.Navigation }
            };
            await Navigation.PushModalAsync(detailInfoPage, true);           
        }
    }

    public abstract class ListPageXaml : ModelBoundContentPage<ListViewModel> { }
}
