using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDotnet
{
    public class GeoCodeResponse
    {
        public string? lat { get; set; }
        public string? lon { get; set; }

        public GeoCodeResponse(string? lat, string? lon)
        {
            this.lat = lat;
            this.lon = lon;
        }   
    }
}
