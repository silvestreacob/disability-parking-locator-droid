﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using dpark.Pages.MapSearch;
using dpark.ViewModels.MapSearch;
using dpark.Pages.List;
using dpark.ViewModels.List;
using dpark.Pages.Submit;
using dpark.Pages.About;
using dpark.ViewModels.About;
using dpark.ViewModels.Base;

using Xamarin.Forms;

namespace dpark.Pages
{
        
    public class RootPage : TabbedPage
    {
        public RootPage()
        {
            Children.Add(new MyNavigationPage(new MapSearchPages
            {
                Title = "Map",
                Icon = new FileImageSource { File = "map.png" },
                BindingContext = new MainViewModel() { Navigation = this.Navigation }
            })
            {
                //Title = "Dpark",
                Icon = new FileImageSource { File = "map.png" }
            });

            Children.Add(new MyNavigationPage(new ListPage
            {
                Title = "List",
                Icon = new FileImageSource { File = "list.png" },
                BindingContext = new ListViewModel() { Navigation = this.Navigation }
            })
            {
                //Title = "Results List",
                Icon = new FileImageSource { File = "list.png" }
            });

            Children.Add(new MyNavigationPage(new WebSubmitPage
            {
                Title = "Submit",
                Icon = new FileImageSource { File = "submit.png" }
                //BindingContext = new ListViewModel() { Navigation = this.Navigation }
            })
            {
                //Title = "Submit Space",
                Icon = new FileImageSource { File = "submit.png" }
            });

            Children.Add(new MyNavigationPage(new AboutPage
            {
                Title = "About",
                Icon = new FileImageSource { File = "about.png" },
                BindingContext = new AboutViewModel() { Navigation = this.Navigation }
            })
            {
                //Title = "About",
                Icon = new FileImageSource { File = "about.png" }
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
            //BarBackgroundColor = Color.Blue;
            BarTextColor = Color.White;
            
        }

        public MyNavigationPage()
        {
            Init();
        }
    }

    public enum MenuType
    {
        MapSearch,
        ResultList,
        Submit,
        About
    }
    public class HomeMenuItem
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.About;
        }

        public string Icon { get; set; }

        public MenuType MenuType { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public int Id { get; set; }
    }
}
