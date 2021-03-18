//Employer Database interaction functions
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
    public class SignUpEmployerDataAccessLayer
    {
        string connectionString;
        private readonly IConfiguration _configuration;

        public SignUpEmployerDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public void Create(SignUpEmployerModel employer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Employers (Employer_Name, Employer_Email, Employer_PW, Employer_Field, Employer_Description, Employer_Status) VALUES(@Employer_Name, @Employer_Email, @Employer_PW, @Employer_Field, @Employer_Description, @Employer_Status);";
                employer.Feedback = "";

                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Employer_Name", employer.Employer_Name);
                        command.Parameters.AddWithValue("@Employer_Email", employer.Employer_Email);
                        command.Parameters.AddWithValue("@Employer_PW", employer.Employer_PW);
                        command.Parameters.AddWithValue("@Employer_Field", employer.Employer_Field);
                        command.Parameters.AddWithValue("@Employer_Description", employer.Employer_Description);
                        command.Parameters.AddWithValue("@Employer_Status", 1);

                        connection.Open();

                        employer.Feedback = command.ExecuteNonQuery().ToString() + " Record Added";

                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    employer.Feedback = "ERROR: " + e.Message;
                }
            }
        }
        public IEnumerable<SignUpEmployerModel> GetActiveRecords()
        {
            List<SignUpEmployerModel> lisTix = new List<SignUpEmployerModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSql = "SELECT * FROM Employers ORDER BY Employer_ID;";
                    SqlCommand cmd = new SqlCommand(strSql, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SignUpEmployerModel employer = new SignUpEmployerModel();
                        employer.Employer_ID = Convert.ToInt32(rdr["Employer_ID"]);
                        employer.Employer_Name = rdr["Employer_Name"].ToString();
                        employer.Employer_Email = rdr["Employer_Email"].ToString();
                        employer.Employer_PW = rdr["Employer_PW"].ToString();
                        employer.Employer_Field = rdr["Employer_Field"].ToString();
                        employer.Employer_Description = rdr["Employer_Description"].ToString();
                        employer.Employer_Status = Boolean.Parse(rdr["Employer_Status"].ToString());
                        lisTix.Add(employer);
                    }
                    con.Close();
                }
            }
            catch (Exception e)
            {

            }
            return lisTix;
        }
        public SignUpEmployerModel GetOneRecord(int? id)
        {
            SignUpEmployerModel Employer = new SignUpEmployerModel();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT * FROM Employers WHERE Employer_ID = @Employer_ID;";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Employer_ID", id);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Employer.Employer_ID = Convert.ToInt32(rdr["Employer_ID"]);
                        Employer.Employer_Name = rdr["Employer_Name"].ToString();
                        Employer.Employer_Email = rdr["Employer_Email"].ToString();
                        Employer.Employer_PW = rdr["Employer_PW"].ToString();
                        Employer.Employer_Field = rdr["Employer_Field"].ToString();
                        Employer.Employer_Description = rdr["Employer_Description"].ToString();
                        Employer.Employer_Status = Boolean.Parse(rdr["Employer_Status"].ToString());
                    }
                    con.Close();
                }
            }

            catch (Exception err)
            {
                Employer.Feedback = "ERROR: " + err.Message;
            }

            return Employer;
        }

        
        public void UpdateEmployer(SignUpEmployerModel tEmployer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    string strSQL;

                    if (tEmployer.Employer_Status == false )
                    {
                        strSQL = "UPDATE Employers SET Employer_Name = @Employer_Name, Employer_Email = @Employer_Email, Employer_PW = @Employer_PW, Employer_Field = @Employer_Field, Employer_Description = @Employer_Description, " + "Employer_Status = @Employer_Status WHERE Employer_ID = @Employer_ID;";
                    }
                    else
                    {
                        strSQL = "UPDATE Employers SET Employer_Name = @Employer_Name, Employer_Email = @Employer_Email, Employer_PW = @Employer_PW, Employer_Field = @Employer_Field, Employer_Description = @Employer_Description, " + "Employer_Status = @Employer_Status WHERE Employer_ID = @Employer_ID;";
                    }

                    cmd.CommandText = strSQL;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Employer_Name", tEmployer.Employer_Name);
                    cmd.Parameters.AddWithValue("@Employer_Email", tEmployer.Employer_Email);
                    cmd.Parameters.AddWithValue("@Employer_PW", tEmployer.Employer_PW);
                    cmd.Parameters.AddWithValue("@Employer_Field", tEmployer.Employer_Field);
                    cmd.Parameters.AddWithValue("@Employer_Description", tEmployer.Employer_Description);
                    cmd.Parameters.AddWithValue("@Employer_Status", tEmployer.Employer_Status);
                    cmd.Parameters.AddWithValue("@Employer_ID", tEmployer.Employer_ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            catch (Exception err)
            {
                tEmployer.Feedback = "ERROR: " + err.Message;
            }
        }

        //Deleting a record 
        public SignUpEmployerModel DeleteEmployer(int? id)
        {
            SignUpEmployerModel Employer = new SignUpEmployerModel();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "DELETE FROM Employers WHERE Employer_ID = @Employer_ID;";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Employer_ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            catch (Exception err)
            {
                Employer.Feedback = "ERROR: " + err.Message;
            }

            return Employer;
        }
        public IEnumerable<SignUpEmployerModel> GetEmployerLogin(SignUpEmployerModel tEmployer)
        {
            List<SignUpEmployerModel> lstEmployer = new List<SignUpEmployerModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT TOP 1 * FROM Employers WHERE Employer_Email = @Employer_Email AND Employer_PW = @Employer_PW;";

                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Employer_Email", tEmployer.Employer_Email);
                    cmd.Parameters.AddWithValue("@Employer_PW", tEmployer.Employer_PW);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SignUpEmployerModel tMatch = new SignUpEmployerModel();
                        tMatch.Employer_ID = Convert.ToInt32(rdr["Employer_ID"]);
                        tMatch.Employer_Email = rdr["Employer_Email"].ToString();
                        tMatch.Employer_PW = rdr["Employer_PW"].ToString();
                        lstEmployer.Add(tMatch);

                    }

                    con.Close();

                }
            }

            catch (Exception err)
            {

            }
            return lstEmployer;
        }
    }
}
