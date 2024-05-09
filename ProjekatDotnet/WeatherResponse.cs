using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDotnet
{
    public class WeatherResponse
    {
        public string? temp { get; set; }
        public string? dateTime { get; set; }
        public string? minTemp { get; set; }
        public string? maxTemp { get; set; }
        public string? description { get; set; }

        public WeatherResponse(string? temp, string? dateTime, string? minTemp, string? maxTemp, string? description)
        {
            this.temp = temp;
            this.dateTime = dateTime;
            this.minTemp = minTemp;
            this.maxTemp = maxTemp;
            this.description = description;
        }
    }
}
