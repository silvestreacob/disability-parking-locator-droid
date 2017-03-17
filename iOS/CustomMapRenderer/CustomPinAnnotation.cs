using MapKit;
using CoreLocation;

namespace dpark.iOS.CustomMapRenderer
{
    public class CustomPinAnnotation : MKAnnotation
    {
        string title;
        public override CLLocationCoordinate2D Coordinate { get { return this.Coords; } }
        public CLLocationCoordinate2D Coords;
        public override string Title { get { return title; } }
        public CustomPinAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle)
        {
            this.Coords = coordinate;
            this.title = title;           
        }
    }
}
