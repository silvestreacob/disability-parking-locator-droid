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
using Plugin.Geolocator;

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

            //919 Ala Moana Blvd
            double lat = 21.2951427;
            double lon = -157.8609393;
       
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var p = await locator.GetPositionAsync(timeoutMilliseconds: 1000);
                lat = p.Latitude;
                lon = p.Longitude;
            }
            catch (Exception) { }

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

                    tmpSpaceData tmp = new tmpSpaceData(spacedata, lat, lon);
                    AppData.Spaces.tmpSpaceCollection.Add(tmp);
                }

                isSuccess = true;
            }
            catch (Exception)
            {
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
            string results = "";

            try
            {
                var token = await GetGeocode(RequestGeoApi + searchaddress + Config.OnSpecificRegion + Config.GmapApikey); //initially search query
                var geoObject = JsonConvert.DeserializeObject<GeoObject>(token);

                if(geoObject.results[0].formatted_address == "Hawaii, USA")
                {
                    var resultwithspace = await GeocodeEnteredAddressWithSpace(searchaddress);
                    return resultwithspace;                    
                }

                results = geoObject.results[0].formatted_address + "&" + geoObject.results[0].geometry.location.lat + "&" + geoObject.results[0].geometry.location.lng + "&" + geoObject.results[0].address_components[1].short_name;
                return results;         
            }

            catch { return ""; }
        }
        #endregion

        async private Task<string> GeocodeEnteredAddressWithSpace(string searchaddress)
        {
            string result = "";

            try
            {
                searchaddress = searchaddress.Replace(",", " "); //search with space _

                var token = await GetGeocode(RequestGeoApi + searchaddress + Config.OnSpecificRegion + Config.GmapApikey);
                var geoObject = JsonConvert.DeserializeObject<GeoObject>(token);

                if(geoObject.results[0].formatted_address == "Hawaii, USA")
                {
                    var resultoutsideregion = await GeocodeEnteredAddressOutsideAdministrativeRegion(searchaddress);
                    return resultoutsideregion;
                }

                result = geoObject.results[0].formatted_address + "&" + geoObject.results[0].geometry.location.lat + "&" + geoObject.results[0].geometry.location.lng + "&" + geoObject.results[0].address_components[1].short_name;
                return result;
            }
            catch { return ""; }
        }

        async private Task<string> GeocodeEnteredAddressOutsideAdministrativeRegion(string searchaddress)
        {
            string result = "";

            try
            {
                searchaddress = searchaddress.Replace(" ", ","); //search outside administrative region

                var token = await GetGeocode(RequestGeoApi + searchaddress + Config.GmapApikey);
                var geoObject = JsonConvert.DeserializeObject<GeoObject>(token);

                if (geoObject.results[0].formatted_address == "Hawaii, USA")
                    return "";

                result = geoObject.results[0].formatted_address + "&" + geoObject.results[0].geometry.location.lat + "&" + geoObject.results[0].geometry.location.lng + "&" + geoObject.results[0].address_components[1].short_name;
                return result;
            }
            catch { return ""; }
        }
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
