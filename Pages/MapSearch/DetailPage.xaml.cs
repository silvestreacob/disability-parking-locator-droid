using dpark.Pages.Base;
using dpark.ViewModels.MapSearch;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dpark.Pages.MapSearch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : DetailPageXaml
    {
        public DetailPage()
        {
            InitializeComponent();
        }
    }

    public abstract class DetailPageXaml : ModelBoundContentPage<DetailInfoViewModel> { }
}
