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
        private const string RequestGeoApi = @"maps/api/geocode/json?address=";

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
                AppData.Spaces.PostsCollection.Clear();

                var token = await Get(RequestSpaces);
                var rootObject = JsonConvert.DeserializeObject<RootObject>(token);

                List<Post> posts = rootObject.posts;
                
                foreach(var post in posts)
                {
                    var serialize = JsonConvert.SerializeObject(post);
                    var deserializePosts = JsonConvert.DeserializeObject<Post>(serialize);

                    SpaceData spacedata = new SpaceData(deserializePosts);
                    AppData.Spaces.PostsCollection.Add(spacedata);
                }

                isSuccess = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                isSuccess = false;
            }

            return isSuccess;
        }

        #region GeocodeEnteredAddress
        async private Task<string> GetGeocode(string request)
        {
#if DEBUG
            var client = new HttpClient { BaseAddress = new Uri(Config.GmapAddress) };
#else
            var client = new HttpClient { BaseAddress = new Uri(Config.GmapAddress), Timeout = new TimeSpan(0, 0, 10)};
#endif

            var response = await client.GetAsync(request);
            return response.Content.ReadAsStringAsync().Result;
        }

         async public Task <string> GeocodeEnteredAddress(string searchaddress)
        {
            searchaddress = searchaddress.Replace(" ", ",");

            try
            {
                var token = await GetGeocode(RequestGeoApi + searchaddress + Config.OnSpecificRegion + Config.GmapApikey);
                var geoObject = JsonConvert.DeserializeObject<GeoObject>(token);

                var formatted_address = geoObject.results[0].formatted_address;
                var lat = geoObject.results[0].geometry.location.lat;
                var lon = geoObject.results[0].geometry.location.lng;
                var name = geoObject.results[0].address_components[1].short_name;
                Debug.WriteLine(formatted_address + "\n" + lat + "\n" + lon + "\n" + name + "\n");

                var results = geoObject.results[0].formatted_address + "&" + geoObject.results[0].geometry.location.lat + "&" + geoObject.results[0].geometry.location.lng + "&" + geoObject.results[0].address_components[1].short_name;
                return results;
            }
            catch { return ""; }
        }
        #endregion
    }
}
