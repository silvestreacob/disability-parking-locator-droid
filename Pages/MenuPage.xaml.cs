using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dpark.ViewModels.Base;

namespace dpark.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        RootPage root;
        List<HomeMenuItem> menuItems;
        public MenuPage(RootPage root)
        {
            this.root = root;
            InitializeComponent();
            BindingContext = new BaseViewModel(Navigation)
            {
                Title = "dpark.us",
                Subtitle = "Disability Parking Locator",
                Icon = "slideout.png"
            };

            ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem { Title = "Map Search", MenuType = MenuType.MapSearch, Icon ="sales.png" },
                    new HomeMenuItem { Title = "Results List", MenuType = MenuType.ResultList, Icon = "customers.png" },
                    new HomeMenuItem { Title = "Submit Space", MenuType = MenuType.Submit, Icon = "products.png" },
                    new HomeMenuItem { Title = "About", MenuType = MenuType.About, Icon = "about.png" },

                };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (ListViewMenu.SelectedItem == null)
                    return;

                await this.root.NavigateAsync(((HomeMenuItem)e.SelectedItem).MenuType);
            };
        }
    }
}