using Xamarin.Forms;
using dpark.ViewModels.Base;

namespace dpark.Pages.Base
{
    public abstract class ModelBoundContentPage<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel
        {
            get { return base.BindingContext as TViewModel; }
        }
        public new TViewModel BindingContext
        {
            set
            {
                base.BindingContext = value;
                base.OnPropertyChanged("BindingContext");
            }
        }
    }
}
