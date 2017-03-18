using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dpark.Models.Data
{
    public class tmpSpaceData
    {
        public tmpSpaceData(SpaceData item, double distance)
        {
            _id = item.ID;
            _title = item.Title;
            _streetaddress = item.StreetAddress;
            _imageurl = item.ImageURL;
            _geolatitude = item.GeoLatitude;
            _geolongitude = item.GeoLongitude;

            var miles = String.Format("{0:0.00}", distance);
            _distance = Convert.ToDouble(miles);
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

        #region Latitude
        private double _geolatitude;
        public double Latitude
        {
            get { return _geolatitude; }
            set { _geolatitude = value; }
        }
        #endregion

        #region Longitude
        private double _geolongitude;
        public double Longitude
        {
            get { return _geolongitude; }
            set { _geolongitude = value; }
        }
        #endregion

        #region DistanceFrom
        private double _distance;
        public double DistanceFrom
        {
            get { return _distance; }
            set { _distance = value; }
        }
        #endregion

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

        public string ApproximateDistance
        {
            get
            {
                var distance = DistanceFrom;
                string extension = " miles away";
                return string.Format("Approximately", distance, extension);
            }
        }
    }
}
