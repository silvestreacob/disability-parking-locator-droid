using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dpark.Pages.Splash
{    
    public partial class Splash : ContentPage
    {
        public Splash()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await App.ExecuteIfConnected(async () =>
            {
                App.GoToRoot();
            });
        }
    }
}