using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL
    {
        private string connectionString;
        private const string SQL_GetSurveys = "select COUNT(sr.parkCode) AS parkCount, sr.parkCode, p.parkName, p.parkDescription FROM survey_result sr JOIN park p ON p.parkCode = sr.parkCode GROUP BY sr.parkCode, p.parkName, p.parkDescription ORDER BY parkCount DESC;";
        private const string SQL_PostSurvey = "INSERT INTO survey_result VALUES (@parkcode, @emailAddress, @state, @activityLevel);";

        public SurveySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Survey> GetSurveys()
        {
            List<Survey> output = new List<Survey>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSurveys, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Survey s = new Survey();
                        s.ParkCode = Convert.ToString(reader["parkCode"]).ToLower();
                        s.ParkName = Convert.ToString(reader["parkName"]);
                        s.ParkCount = Convert.ToInt32(reader["parkCount"]);
                        s.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        
                        output.Add(s);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return output;
        }

        public bool SubmitSurvey(SurveyForm survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_PostSurvey, conn);
                    cmd.Parameters.AddWithValue("@parkcode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }
    }
}