using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class WeatherInformation
    {
        //[JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        //[JsonPropertyName("cod")]
        public int Cod { get; set; }

        //[JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        //[JsonPropertyName("name")]
        public string City { get; set; }

        //[JsonPropertyName("country")]
        public string Country { get; set; }

        //[JsonPropertyName("Wind")]
        public double WindSpeed { get; set; }

        //[JsonPropertyName("temp")]
        public double Temperature { get; set; }
    }
}
