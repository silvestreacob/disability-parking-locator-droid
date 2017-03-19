using dpark.Views.Base;
using dpark.ViewModels.MapSearch;

namespace dpark.Views.MapSearch
{
    public partial class DetailPageHeaderView : DetailPageHeaderViewXaml
    {
        public DetailPageHeaderView()
        {
            InitializeComponent();
        }
    }

    public abstract class DetailPageHeaderViewXaml : ModelBoundContentView<DetailInfoViewModel> { }
}
