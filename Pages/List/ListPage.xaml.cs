using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dpark.Pages.Base;
using dpark.ViewModels.List;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dpark.Pages.List
{    
    public partial class ListPage : ListPageXaml
    {
        public ListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.LoadSpaces.Execute(null);
        }
    }

    public abstract class ListPageXaml : ModelBoundContentPage<ListViewModel> { }
}
