using dpark.Models.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dpark.Models.Data
{
    public class MapPinData : NotifyPropertyChanged
    {
        public MapPinData()
        {

        }

        public MapPinData(SpaceData Item) : this()
        {
            _title = Item.Title;
            _streetaddress = Item.StreetAddress;
            _geolatitude = Item.GeoLatitude;
            _geolongitude = Item.GeoLongitude;

            _imageurl = Config.ServerAddress + Item.ImageURL;
        }

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
    }
}
