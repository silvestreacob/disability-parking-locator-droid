using Xamarin.Forms;
using Xamarin.Forms.Maps;
using dpark.Pages.Base;
using dpark.Models;
using dpark.ViewModels.MapSearch;
using dpark.CustomRenderer;
using System.Diagnostics;
using System.ComponentModel;

namespace dpark.Pages.MapSearch
{
    public class MainMapPage : ModelBoundContentPage<MainViewModel>
    {
        
        public MainMapPage()
        {
            BindingContext = new MainViewModel();

            //customMap = new CustomMap()
            //{
            //    IsShowingUser = true,
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};

            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(5)));

            //foreach(var item in AppData.Spaces.MapPinCollection)
            //{
            //    customMap.PinList.Add(item);
            //}

            Map customMap = new Map()
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            customMap.Pins.Clear();

            foreach (var item in AppData.Spaces.PostsCollection)
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(item.GeoLatitude, item.GeoLongitude),
                    Label = item.Title,
                    Address = item.StreetAddress
                };

                customMap.Pins.Add(pin);
            }


            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.300, -157.8167), Distance.FromMiles(5)));

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: customMap,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            Content = relativeLayout;

            Debug.WriteLine(AppData.Spaces.PostsCollection.Count);
        }

    }
}
