using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.DAL;

namespace Capstone.Web.Models
{
    public class Park
    {
        private string connectionString;

        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int Acreage { get; set; }
        public int ElevationInFeet { get; set; }
        public int MilesOfTrail { get; set; }
        public int NumerOfCampsites { get; set; }
        public string Climate { get; set; }
        public int YearFounded { get; set; }
        public int AnnualVisitorCount { get; set; }
        public string InspirationalQuote { get; set; }
        public string InspirationalQuoteSource { get; set; }
        public string ParkDescription { get; set; }
        public int EntryFee { get; set; }
        public int NumberOfAnimalSpecies { get; set; }
        //To allow for Farenheit to Celcius conversions
        public bool IsFarenheit { get; set; }

        public List<Weather> ParkForecast
        {
            get
            {
                List<Weather> output = new List<Weather>();
                WeatherSqlDAL weatherDAL = new WeatherSqlDAL(connectionString, IsFarenheit);

                output = weatherDAL.GetForecast(ParkCode);

                return output;
            }
        }

        public Park(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}