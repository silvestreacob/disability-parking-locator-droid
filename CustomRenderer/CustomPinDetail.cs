using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace dpark.CustomRenderer
{
    public class CustomPinDetail : AbsoluteLayout
    {
        public CustomPinDetail()
        {
            txtTitle = new Label
            {
                Text = Title,
                //Style = Add Later
            };

            imgShowDetail = new Image
            {
                Source = DetailIcon,
                HorizontalOptions = LayoutOptions.Start
            };

            var spaceInfo = new StackLayout
            {
                Spacing = Device.OnPlatform(3,3,3),
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    txtTitle,
                    imgShowDetail
                }
            };

            Children.Add(spaceInfo);
        }

        private Label txtTitle;
        private Image imgShowDetail;

        #region txtTitle
        private string _title = "title";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                txtTitle.Text = _title;
            }
        }
        #endregion

        #region imgShowDetail
        private string _detailIcon = "";
        public string DetailIcon
        {
            get { return _detailIcon; }
            set
            {
                _detailIcon = value;
                imgShowDetail.Source = _detailIcon;
            }
        }
        #endregion
    }
}
