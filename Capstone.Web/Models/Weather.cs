using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public string Low { get; set; }
        //LowInterger and HighInteger added to allow for successful WarningMessages since database is given in Farenheit
        public int LowInteger { get; set; }
        public string High { get; set; }
        public int HighInteger { get; set; }
        public string Forecast { get; set; }

        public List<string> WarningMessages
        {
            get
            {
                List<string> output = new List<string>();
                if (Forecast == "snow")
                {
                    output.Add("Pack snowshoes!");
                }
                else if (Forecast == "rain")
                {
                    output.Add("Pack rain gear and waterproof shoes!");
                }
                else if (Forecast == "thunderstorms")
                {
                    output.Add("Seek shelter and avoid hiking on exposed ridges!");
                }
                else if (Forecast == "sun")
                {
                    output.Add("Pack sunblock!");
                }
                if (HighInteger > 75)
                {
                    output.Add("Bring an extra gallon of water!");
                }
                if (LowInteger < 20)
                {
                    output.Add("Being exposed to frigid temperatures is dangerous. Beware!");
                }
                if (Math.Abs(HighInteger - LowInteger) > 20)
                {
                    output.Add("Wear breathable layers!");
                }
                return output;            
            }
        }

        public Weather()
        {
        
        }

        public int FarenheitToCelcius(int temp)
        {
                return (int)((temp - 32) / 1.8);
        }
    }
}