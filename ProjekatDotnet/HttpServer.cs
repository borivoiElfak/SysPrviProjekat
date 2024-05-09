using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDotnet
{
    internal class HttpServer
    {

        private HttpListener listener;
        private static string localhost = "http://localhost:5050/";
        
        private static bool first = true;

        public HttpServer() {

            listener = new HttpListener();
            listener.Prefixes.Add(localhost);
        }

        public void startServer() {


            listener.Start();
            while (true) 
                ThreadPool.QueueUserWorkItem(ServeRequest, listener.GetContext());

        }

        private static void ServeRequest(object? state)
        {

            if (state == null)
                return;

            var context = (HttpListenerContext)state;

            try
            {
                var query = context.Request
                                         .Url?
                                         .Query
                                         .Remove(0, 1)
                                         .ToString();

                if (query == null)
                    throw new Exception("Invalid Query");

                var array = query.Split("=");
                if (array.Length != 2)
                    throw new Exception("Invalid Query");


                query = array[1];


                Console.WriteLine(query);

                if (query == null)
                    throw new Exception("Null query exc");

                GeoCodeResponse geoCode = GeoCodeApiService.getCoordinates(query);


                var key = $"{geoCode.lat}:{geoCode.lon}";
                Console.WriteLine();

                if (Cache.Contains(key))
                {
                    Console.WriteLine("From Cache!");
                    print(Cache.ReadFromCache(key));
                }
                else
                {
                    List<WeatherResponse> response = WeatherApiService.getWeather(geoCode);
                    Cache.WriteToCache(key, response);
                    print(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void print(List<WeatherResponse> data)
        {

            foreach (var weather in data)
            {

                Console.WriteLine($"{weather.dateTime}  -- {weather.description}");
                Console.WriteLine($"Temp: {weather.temp} C");
                Console.WriteLine($"Min Temp: {weather.minTemp}");
                Console.WriteLine($"Max Temp: {weather.maxTemp}");
                Console.WriteLine();

            }

            Console.WriteLine("\n");

        }

    }
}
