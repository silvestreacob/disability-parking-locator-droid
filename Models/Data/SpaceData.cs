
using System;
using System.Diagnostics;
using System.Globalization;
using dpark.Models.WebService;

namespace dpark.Models.Data
{
    public class SpaceData : NotifyPropertyChanged
    {
        public SpaceData()
        {
            _id = _title = _streetaddress = _imageurl = _urladdress = string.Empty;
            _geolatitude = _geolongitude = 0.0;
        }
        public SpaceData(Post Item) : this()
        {
            _id = Item.ID;
            _title = Item.title;

            foreach (var item in Item.metadata)
            {
                #region _streetaddress
                if (item.key.Contains("street_address") && item.value != string.Empty)
                    _streetaddress = item.value;

                else if (item.key.Contains("geo_address") && item.value != string.Empty)
                    _streetaddress = item.value;
                #endregion

                #region _geolatitude
                if (item.key.Contains("geo_latitude") && item.value != string.Empty)
                    _geolatitude = Convert.ToDouble(item.value);

                else if (item.key.Contains("lat") && item.value != string.Empty)
                    _geolatitude = Convert.ToDouble(item.value);
                #endregion

                #region _geolongitude
                if ((item.key.Contains("geo_longitude") && item.value != string.Empty) || (item.key.Contains("lon") && item.value != string.Empty))
                    _geolongitude = Convert.ToDouble(item.value);
                #endregion
            }

            _imageurl = Config.ServerAddress + Item.featured_image;
            _urladdress = Item.URL;

#if DEBUG
            Debug.WriteLine(_id + "\n" + _title + "\n" + _streetaddress + "\n" + _geolatitude + " " + _geolongitude + "\n" + _imageurl + "\n" + _urladdress + "\n");
#endif

        }
        #region ID
        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region Title
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        #endregion

        #region Latitude
        private double _geolatitude;
        public double GeoLatitude
        {
            get { return _geolatitude; }
            set { _geolatitude = value; }
        }
        #endregion

        #region Longitude
        private double _geolongitude;
        public double GeoLongitude
        {
            get { return _geolongitude; }
            set { _geolongitude = value; }
        }
        #endregion

        #region StreetAddress
        private string _streetaddress;
        public string StreetAddress
        {
            get { return _streetaddress; }
            set { _streetaddress = value; }
        }
        #endregion

        #region ImageURL
        private string _imageurl;
        public string ImageURL
        {
            get { return _imageurl; }
            set { _imageurl = value; }
        }
        #endregion

        #region ThumbnailImageURL
        public string ThumbnailImageUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImageURL) || !ImageURL.Contains("."))
                    return null;

                var index = ImageURL.LastIndexOf('.');
                var name = ImageURL.Substring(0, index);
                var extension = ImageURL.Substring(index);
                return string.Format(name, "-125x125", extension);
            }
        }
        #endregion

        #region URLAddress
        private string _urladdress;
        public string UrlAddress
        {
            get { return _urladdress; }
            set { _urladdress = value; }
        }
        #endregion
    }
}
