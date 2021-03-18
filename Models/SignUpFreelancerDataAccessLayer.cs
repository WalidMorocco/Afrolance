//Freelancer Database interaction functions
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Afrolance.Models;

using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace Afrolance.Models
{
    public class SignUpFreelancerDataAccessLayer
    {
        string connectionString;
        private readonly IConfiguration _configuration;

        public SignUpFreelancerDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public void Create(SignUpFreelancerModel Freelancer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Freelancers (Freelancer_FName, Freelancer_LName, Freelancer_Email, Freelancer_PW, Freelancer_Field, Freelancer_Resume, Freelancer_Phone, Freelancer_Country, Freelancer_City, Freelancer_Status) VALUES(@Freelancer_FName,  @Freelancer_LName, @Freelancer_Email, @Freelancer_PW, @Freelancer_Field, @Freelancer_Resume, @Freelancer_Phone, @Freelancer_Country, @Freelancer_City, @Freelancer_Status);";
                Freelancer.Feedback = "";

                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Freelancer_FName", Freelancer.Freelancer_FName);
                        command.Parameters.AddWithValue("@Freelancer_LName", Freelancer.Freelancer_LName);
                        command.Parameters.AddWithValue("@Freelancer_Email", Freelancer.Freelancer_Email);
                        command.Parameters.AddWithValue("@Freelancer_PW", Freelancer.Freelancer_PW);
                        command.Parameters.AddWithValue("@Freelancer_Field", Freelancer.Freelancer_Field);
                        command.Parameters.AddWithValue("@Freelancer_Resume", Freelancer.Freelancer_Resume);
                        command.Parameters.AddWithValue("@Freelancer_Bio", Freelancer.Freelancer_Bio);
                        command.Parameters.AddWithValue("@Freelancer_Phone", Freelancer.Freelancer_Phone);
                        command.Parameters.AddWithValue("@Freelancer_Country", Freelancer.Freelancer_Country);
                        command.Parameters.AddWithValue("@Freelancer_City", Freelancer.Freelancer_City);
                        command.Parameters.AddWithValue("@Freelancer_Status", 1);

                        connection.Open();

                        Freelancer.Feedback = command.ExecuteNonQuery().ToString() + " Record Added";

                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Freelancer.Feedback = "ERROR: " + e.Message;
                }
            }
        }
        public IEnumerable<SignUpFreelancerModel> GetActiveRecords()
        {
            List<SignUpFreelancerModel> lisTix = new List<SignUpFreelancerModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSql = "SELECT * FROM Freelancers ORDER BY Freelancer_LName;";
                    SqlCommand cmd = new SqlCommand(strSql, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SignUpFreelancerModel Freelancer = new SignUpFreelancerModel();
                        Freelancer.Freelancer_ID = Convert.ToInt32(rdr["Freelancer_ID"]);
                        Freelancer.Freelancer_FName = rdr["Freelancer_FName"].ToString();
                        Freelancer.Freelancer_LName = rdr["Freelancer_LName"].ToString();
                        Freelancer.Freelancer_Email = rdr["Freelancer_Email"].ToString();
                        Freelancer.Freelancer_PW = rdr["Freelancer_PW"].ToString();
                        Freelancer.Freelancer_Field = rdr["Freelancer_Field"].ToString();
                        Freelancer.Freelancer_Resume = rdr["Freelancer_Resume"].ToString();
                        Freelancer.Freelancer_Bio = rdr["Freelancer_Bio"].ToString();
                        Freelancer.Freelancer_Phone = rdr["Freelancer_Phone"].ToString();
                        Freelancer.Freelancer_Country = rdr["Freelancer_Country"].ToString();
                        Freelancer.Freelancer_City = rdr["Freelancer_City"].ToString();
                        Freelancer.Freelancer_Status = Boolean.Parse(rdr["Freelancer_Status"].ToString());
                        lisTix.Add(Freelancer);
                    }
                    con.Close();
                }
            }
            catch (Exception e)
            {

            }
            return lisTix;
        }
        public SignUpFreelancerModel GetOneRecord(int? id)
        {
            //Placeholder for record based on ID
            SignUpFreelancerModel Freelancer = new SignUpFreelancerModel();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT * FROM Freelancers WHERE Freelancer_ID = @Freelancer_ID;";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Freelancer_ID", id);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Freelancer.Freelancer_ID = Convert.ToInt32(rdr["Freelancer_ID"]);
                        Freelancer.Freelancer_FName = rdr["Freelancer_FName"].ToString();
                        Freelancer.Freelancer_LName = rdr["Freelancer_LName"].ToString();
                        Freelancer.Freelancer_Email = rdr["Freelancer_Email"].ToString();
                        Freelancer.Freelancer_PW = rdr["Freelancer_PW"].ToString();
                        Freelancer.Freelancer_Field = rdr["Freelancer_Field"].ToString();
                        Freelancer.Freelancer_Resume = rdr["Freelancer_Resume"].ToString();
                        Freelancer.Freelancer_Bio = rdr["Freelancer_Bio"].ToString();
                        Freelancer.Freelancer_Phone = rdr["Freelancer_Phone"].ToString();
                        Freelancer.Freelancer_Country = rdr["Freelancer_Country"].ToString();
                        Freelancer.Freelancer_City = rdr["Freelancer_City"].ToString();
                        Freelancer.Freelancer_Status = Boolean.Parse(rdr["Freelancer_Status"].ToString());
                    }
                    con.Close();
                }
            }

            catch (Exception err)
            {
                Freelancer.Feedback = "ERROR: " + err.Message;
            }

            return Freelancer;
        }


        public void UpdateFreelancer(SignUpFreelancerModel tFreelancer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    string strSQL;

                    if (tFreelancer.Freelancer_Status == false)
                    {
                        strSQL = "UPDATE Freelancers SET Freelancer_FName = @Freelancer_Name, Freelancer_LName = @Freelancer_Lame, Freelancer_Email = @Freelancer_Email, Freelancer_PW = @Freelancer_PW, Freelancer_Field = @Freelancer_Field, Freelancer_Resume = @Freelancer_Resume, Freelancer_Bio = @Freelancer_Bio, Freelancer_Phone = @Freelancer_Phone, Freelancer_Country = @Freelancer_Country, Freelancer_City = @Freelancer_City, " + "Freelancer_Status = @Freelancer_Status WHERE Freelancer_ID = @Freelancer_ID;";
                    }
                    else
                    {
                        strSQL = "UPDATE Freelancers SET Freelancer_FName = @Freelancer_Name, Freelancer_LName = @Freelancer_Lame, Freelancer_Email = @Freelancer_Email, Freelancer_PW = @Freelancer_PW, Freelancer_Field = @Freelancer_Field, Freelancer_Resume = @Freelancer_Resume, Freelancer_Bio = @Freelancer_Bio, Freelancer_Phone = @Freelancer_Phone, Freelancer_Country = @Freelancer_Country, Freelancer_City = @Freelancer_City, " + "Freelancer_Status = @Freelancer_Status WHERE Freelancer_ID = @Freelancer_ID;";
                    }

                    cmd.CommandText = strSQL;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Freelancer_FName", tFreelancer.Freelancer_FName);
                    cmd.Parameters.AddWithValue("@Freelancer_LName", tFreelancer.Freelancer_LName);
                    cmd.Parameters.AddWithValue("@Freelancer_Email", tFreelancer.Freelancer_Email);
                    cmd.Parameters.AddWithValue("@Freelancer_PW", tFreelancer.Freelancer_PW);
                    cmd.Parameters.AddWithValue("@Freelancer_Field", tFreelancer.Freelancer_Field);
                    cmd.Parameters.AddWithValue("@Freelancer_Resume", tFreelancer.Freelancer_Resume);
                    cmd.Parameters.AddWithValue("@Freelancer_Bio", tFreelancer.Freelancer_Bio);
                    cmd.Parameters.AddWithValue("@Freelancer_Phone", tFreelancer.Freelancer_Phone);
                    cmd.Parameters.AddWithValue("@Freelancer_Country", tFreelancer.Freelancer_Country);
                    cmd.Parameters.AddWithValue("@Freelancer_City", tFreelancer.Freelancer_City);
                    cmd.Parameters.AddWithValue("@Freelancer_Status", 1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            catch (Exception err)
            {
                tFreelancer.Feedback = "ERROR: " + err.Message;
            }
        }

        //Deleting a record 
        public SignUpFreelancerModel DeleteFreelancer(int? id)
        {
            SignUpFreelancerModel Freelancer = new SignUpFreelancerModel();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "DELETE FROM Freelancers WHERE Freelancer_ID = @Freelancer_ID;";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Freelancer_ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            catch (Exception err)
            {
                Freelancer.Feedback = "ERROR: " + err.Message;
            }

            return Freelancer;
        }
        public IEnumerable<SignUpFreelancerModel> GetFreelancerLogin(SignUpFreelancerModel lFreelancer)
        {
            List<SignUpFreelancerModel> lstFreelancer = new List<SignUpFreelancerModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT TOP 1 * FROM Freelancers WHERE Freelancer_Email = @Freelancer_Email AND Freelancer_PW = @Freelancer_PW;";

                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Freelancer_Email", lFreelancer.Freelancer_Email);
                    cmd.Parameters.AddWithValue("@Freelancer_PW", lFreelancer.Freelancer_PW);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SignUpFreelancerModel tMatch = new SignUpFreelancerModel();
                        tMatch.Freelancer_ID = Convert.ToInt32(rdr["Freelancer_ID"]);
                        tMatch.Freelancer_Email = rdr["Freelancer_Email"].ToString();
                        tMatch.Freelancer_PW = rdr["Freelancer_PW"].ToString();
                        lstFreelancer.Add(tMatch);

                    }

                    con.Close();

                }
            }

            catch (Exception err)
            {

            }
            return lstFreelancer;
        }
    }
}
