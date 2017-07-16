using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL
    {
        private string connectionString;
        private bool IsFarenheit;
        private const string SQL_GetParkForecast = "SELECT w.* FROM weather w JOIN park p on p.parkCode = w.parkCode WHERE p.parkCode = @parkCode;";

        public WeatherSqlDAL(string connectionString, bool isFarenheit)
        {
            this.connectionString = connectionString;
            IsFarenheit = isFarenheit;
        }

        public List<Weather> GetForecast (string parkCode)
        {

            List<Weather> output = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkForecast, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int high = Convert.ToInt32(reader["high"]);
                        int low = Convert.ToInt32(reader["low"]);

                        Weather w = new Weather();
                        w.ParkCode = parkCode;
                        w.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        w.Low = IsFarenheit ? ToString(low) : ToString(w.FarenheitToCelcius(low));
                        w.LowInteger = low;
                        w.High = IsFarenheit ? ToString(high) : ToString(w.FarenheitToCelcius(high));
                        w.HighInteger = high;
                        w.Forecast = Convert.ToString(reader["forecast"]);

                        output.Add(w);

                    }
                    return output;
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        //Overload ToString method to successfully show the temperature signifier in ParkDetails view
        public string ToString(int temp)
        {
            if (IsFarenheit == true)
            {
                return $"{temp}°F";
            }
            else
            {
                return $"{temp}°C";
            }
        }
    }
}