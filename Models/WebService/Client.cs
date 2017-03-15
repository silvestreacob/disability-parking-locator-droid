using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using dpark.Models.Data;

namespace dpark.Models.WebService
{
    public class Client : IClient
    {
        private const string RequestSpaces = @"allspaces.json";
        private const string RequestDev = @"wp-json/posts?type=space";

        async public Task<bool> CheckConnection()
        {
            return true;
        }

        async private Task<string> Get(string request)
        {
            #if DEBUG
            var client = new HttpClient { BaseAddress = new Uri(Config.ServerAddress) };
            #else
            var client = new HttpClient { BaseAddress = new Uri(Config.ServerAddress), Timeout = new TimeSpan(0, 0, 10)};
            #endif

            var response = await client.GetAsync(request);
            return response.Content.ReadAsStringAsync().Result;
        }
        async public Task<bool> GetSpaces()
        {
            bool isSuccess = false;

            try
            {
                //dont forget to clear existing data collection
                AppData.Spaces.Posts.Clear();

                var token = await Get(RequestSpaces);
                var rootObject = JsonConvert.DeserializeObject<RootObject>(token);

                foreach(var post in rootObject.posts)
                {
                    var deserializespace = JsonConvert.DeserializeObject<DeserializeSpace>(rootObject.ToString());
                    SpaceData spacedata = new SpaceData(deserializespace);
                }
               
                #if DEBUG
                Debug.WriteLine(rootObject);
                #endif
               
                isSuccess = true;
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
