using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ProjekatDotnet
{
    public class GeoCodeApiService
    {
        private static string cityParam = "city";
        private static string limitParam = "1";
        private static string keyParam = "4529a445c17f89419ba605b488c3cb8c";
 

        public static readonly HttpClient HttpClient = new();


        public static GeoCodeResponse getCoordinates(string cityName) {

            try {
                
                var url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityParam}&limit={limitParam}&appid={keyParam}";

                url = url.Replace(cityParam, cityName);

                var response = HttpClient.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"ERROR: {response.StatusCode}");

                var content = response.Content.ReadAsStringAsync().Result;
                var jsonResponse = JArray.Parse(content).First;

                if (jsonResponse == null)
                    throw new Exception("ERROR: City doesn't exist!");
                else {
                    GeoCodeResponse geoCode = new GeoCodeResponse(
                            (string?)jsonResponse["lat"],
                            (string?)jsonResponse["lon"]
                        );
                       
                    if (geoCode.lat == null || geoCode.lon == null)
                           throw new Exception("Invalid Coordinates");

                    return geoCode;
                }

            } catch(Exception e) {
                throw e; 
            }

        }

    }
}
