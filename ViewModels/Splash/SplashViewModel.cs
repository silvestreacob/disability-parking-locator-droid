using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using dpark.ViewModels.Base;
using dpark.Models;

namespace dpark.ViewModels.Splash
{
    public class SplashViewModel : BaseViewModel
    {
        public async Task<bool> IsLoadSpaceData()
        {
            bool isSuccess = false;
            
            try
            {
                isSuccess = await AppData.Spaces.LoadSpaces();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;           
        }
    }
}
