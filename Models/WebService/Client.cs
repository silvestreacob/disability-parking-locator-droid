using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using dpark.Models.Data;

using Xamarin.Forms.Maps;
using System.Text.RegularExpressions;

namespace dpark.Models.WebService
{
    public class Client : IClient
    {
        private const string RequestSpaces = @"allspaces.json";
        private const string RequestDev = @"wp-json/posts?type=space";
        private const string RequestGeoApi = @"maps/api/geocode/json?address=";
        private const string RequestReverseGeo = @"maps/api/geocode/json?latlng=";
        async public Task<bool> CheckConnection()
        {
            await Task.Delay(0);
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

                    tmpSpaceData tmp = new tmpSpaceData(spacedata, 0.0);
                    AppData.Spaces.tmpSpaceCollection.Add(tmp);
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
            var client = new HttpClient { BaseAddress = new Uri(Config.GmapAddress), Timeout = new TimeSpan(0, 0, 10)};
            var response = await client.GetAsync(request);
            return response.Content.ReadAsStringAsync().Result;
        }

         async public Task <string> GeocodeEnteredAddress(string searchaddress)
        {
            searchaddress = searchaddress.Replace(" ", ",");
            string results = "";

            try
            {
                var token = await GetGeocode(RequestGeoApi + searchaddress + Config.OnSpecificRegion + Config.GmapApikey); //initially search query
                var geoObject = JsonConvert.DeserializeObject<GeoObject>(token);

                if(geoObject.results[0].formatted_address == "Hawaii, USA")
                {
                    searchaddress = searchaddress.Replace(",", " "); //search with space _

                    token = await GetGeocode(RequestGeoApi + searchaddress + Config.OnSpecificRegion + Config.GmapApikey);
                    geoObject = JsonConvert.DeserializeObject<GeoObject>(token);

                    if (geoObject.results[0].formatted_address == "Hawaii, USA")
                    {
                        searchaddress = searchaddress.Replace(" ", ","); //search outside the region

                        token = await GetGeocode(RequestGeoApi + searchaddress + Config.GmapApikey);
                        geoObject = JsonConvert.DeserializeObject<GeoObject>(token);
 
                        results = geoObject.results[0].formatted_address + "&" + geoObject.results[0].geometry.location.lat + "&" + geoObject.results[0].geometry.location.lng + "&" + geoObject.results[0].address_components[1].short_name;
                        return results;
                    }

                    results = geoObject.results[0].formatted_address + "&" + geoObject.results[0].geometry.location.lat + "&" + geoObject.results[0].geometry.location.lng + "&" + geoObject.results[0].address_components[1].short_name;
                    return results;
                }

                results = geoObject.results[0].formatted_address + "&" + geoObject.results[0].geometry.location.lat + "&" + geoObject.results[0].geometry.location.lng + "&" + geoObject.results[0].address_components[1].short_name;
                return results;         
            }

            catch { return ""; }
        }
        #endregion

        #region ReverseGeocoding
        async public Task<string> ReverseGeoCoding(double lat, double lng)
        {
            Geocoder geo = new Geocoder();
            Position p = new Position(lat, lng);

            try
            {

                string result="";

                var addresses = await geo.GetAddressesForPositionAsync(p);
                foreach(var address in addresses)
                    result += address;

                //result = Regex.Replace(result, @"\t|\n|\r", "&");

                string[] index = result.Split('\n');
                string newaddress = index[0];
            
                return newaddress;
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}
