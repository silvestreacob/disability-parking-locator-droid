using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace dpark.Pages
{
    public class RootPage : ContentPage
    {
        public RootPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello Page" }
                }
            };
        }
    }

    public class RootTabPage : TabbedPage
    {
        public RootTabPage()
        {
            Children.Add(new MyNavigationPage(new MapSearch.MapSearchPage
            {
                Title = "Map",
                //Icon = new FileImageSource { File = "search.png" }
            })
            {
                Title = "Map",
                //Icon = new FileImageSource { File = "search.png" }
            });
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            this.Title = this.CurrentPage.Title;
        }

    }

    public class MyNavigationPage : NavigationPage
    {
        public MyNavigationPage(Page root)
            : base(root)
        {
            Init();
        }

        void Init()
        {
            BarBackgroundColor = Color.Blue;
            BarTextColor = Color.White;
        }

        public MyNavigationPage()
        {
            Init();
        }
    }
}
