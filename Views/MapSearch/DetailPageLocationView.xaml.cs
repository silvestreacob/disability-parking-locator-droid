using dpark.Views.Base;
using dpark.ViewModels.MapSearch;
using dpark.Statics;

using Xamarin.Forms;

namespace dpark.Views.MapSearch
{
    public partial class DetailPageLocationView : DetailPageLocationViewXaml
    {
        public DetailPageLocationView()
        {
            InitializeComponent();          
        }
    }

    public abstract class DetailPageLocationViewXaml : ModelBoundContentView<DetailInfoViewModel> { }
}
