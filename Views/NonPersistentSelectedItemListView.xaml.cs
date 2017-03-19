using Xamarin.Forms;

namespace dpark.Views
{
    public partial class NonPersistentSelectedItemListView : ListView
    {
        public NonPersistentSelectedItemListView()
        {
            InitializeComponent();
            ItemSelected += (sender, e) => SelectedItem = null;
        }
    }
}
