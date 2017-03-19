using Xamarin.Forms;
using dpark.ViewModels.Base;

namespace dpark.Views.Base
{
    public abstract class ModelBoundContentView<TViewModel> : ContentView where TViewModel : BaseViewModel
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
