using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using dpark.Pages.List;
using dpark.ViewModels.List;

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

            Children.Add(new MyNavigationPage(new ListPage
            {
                Title = "List",
                //Icon = new FileImageSource { File = "about.png" },
                BindingContext = new ListViewModel() { Navigation = this.Navigation }
            })
            {
                Title = "List",
                //Icon = new FileImageSource { File = "about.png" }
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
