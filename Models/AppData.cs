using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dpark.Models.Data;
using dpark.Models.WebService;

namespace dpark.Models
{
    static class AppData
    {
        public class SpaceContainer
        {
            public SpaceContainer()
            {
                _posts = new List<SpaceData>();
            }

            #region Post Collection
            private List<SpaceData> _posts;
            public List<SpaceData> Posts
            {
                get { return _posts; }
                //set { _posts = value; }
            }
            #endregion

            private Client ServiceProvider { get; set; }

            async public Task<bool> LoadSpaces()
            {
                bool isSuccess = false;

                try
                {
                    _posts.Clear();
                    ServiceProvider = new Client();
                    isSuccess = await ServiceProvider.GetSpaces();
                }
                catch (Exception)
                {
                    isSuccess = false;
                }
                
                return isSuccess;
            }

        }

        static public SpaceContainer Spaces = new SpaceContainer();
    }
}
