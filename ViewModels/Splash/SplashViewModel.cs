using System;
using System.Diagnostics;
using Xamarin.Forms;
using System.Threading.Tasks;
using dpark.ViewModels.Base;
using dpark.Models;
using dpark.Models.WebService;

namespace dpark.ViewModels.Splash
{
    public class SplashViewModel : BaseViewModel
    {
        private Client ServiceProvider { get; set; }

        public async Task<bool> IsLoadSpaceData()
        {
            bool isSuccess = false;
            
            try
            {
                ServiceProvider = new Client();
                isSuccess = await ServiceProvider.GetSpaces();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

#if DEBUG
            Debug.WriteLine(isSuccess);
#endif 
            return isSuccess;           
        }
    }
}
