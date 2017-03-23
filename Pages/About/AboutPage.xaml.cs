using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dpark.Pages.Base;
using dpark.ViewModels.About;

namespace dpark.Pages.About
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : AboutPageXaml
    {
        public AboutPage()
        {
            InitializeComponent();
        }
    }

    public abstract class AboutPageXaml: ModelBoundContentPage<AboutViewModel> { }
}
