using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDotnet
{
    
    public class WeatherApiService
    {

        private static string latParam = "latParam";
        private static string lonParam = "lonParam";
        private static string keyParam = "4529a445c17f89419ba605b488c3cb8c";
        private static string unitParam = "metric";

        public static readonly HttpClient HttpClient = new();

        public static List<WeatherResponse> getWeather(GeoCodeResponse geoCode)
        {

            try
            {
                var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={latParam}&lon={lonParam}&appid={keyParam}&units={unitParam}";

                url = url.Replace(latParam, geoCode.lat);
                url = url.Replace(lonParam, geoCode.lon);

                var response = HttpClient.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"ERROR: {response.StatusCode}");

                var content = response.Content.ReadAsStringAsync().Result;
                var jsonResponseList = JObject.Parse(content)["list"];

                List<WeatherResponse> weatherList = new List<WeatherResponse>();

                if(jsonResponseList != null) {

                    foreach (var jsonObject in jsonResponseList)
                    {
                        var main = jsonObject["main"];
                        var weather = jsonObject["weather"]?.First;

                        if (main == null || weather == null)
                            throw new Exception();

                        weatherList.Add(
                                new WeatherResponse(
                                        (string?)main["temp"],
                                        (string?)jsonObject["dt_txt"],
                                        (string?)main["temp_min"],
                                        (string?)main["temp_max"],
                                        (string?)weather["main"]
                                    )
                            );
                    }

                }

                return weatherList;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}

