using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Collections.Generic;
using dpark.Models.Data;

namespace dpark.CustomRenderer
{
    public class CustomMap : Map
    {
        private AbsoluteLayout _mapLayout;
        public AbsoluteLayout MapLayout
        {
            get { return _mapLayout; }
        }
        public CustomMap()
        {
            _mapLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0f, 0f, 1f, 1f));
            _mapLayout.Children.Add(this);

            customPinDetail = new CustomPinDetail();
            customPinDetail.IsVisible = false;

            AbsoluteLayout.SetLayoutFlags(customPinDetail, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(customPinDetail, new Rectangle(0f, 0f, 1f, 1f));

            _mapLayout.Children.Add(customPinDetail);
        }

        public List<MapPinData> PinList = new List<MapPinData>();

        private CustomPinDetail customPinDetail;
    }
}
