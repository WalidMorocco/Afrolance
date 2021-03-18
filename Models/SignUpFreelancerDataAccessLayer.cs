using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Afrolance.Models;
using Microsoft.Extensions.Configuration;

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
                string sql = "INSERT INTO Freelancers (Freelancer_Name, Freelancer_Email, Freelancer_PW, Freelancer_Field, Freelancer_Description, Freelancer_Status) VALUES(@Freelancer_Name, @Freelancer_Email, @Freelancer_PW, @Freelancer_Field, @Freelancer_Description, @Freelancer_Status);";
                Freelancer.Feedback = "";

                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Freelancer_Name", Freelancer.Freelancer_Name);
                        command.Parameters.AddWithValue("@Freelancer_Email", Freelancer.Freelancer_Email);
                        command.Parameters.AddWithValue("@Freelancer_PW", Freelancer.Freelancer_PW);
                        command.Parameters.AddWithValue("@Freelancer_Field", Freelancer.Freelancer_Field);
                        command.Parameters.AddWithValue("@Freelancer_Description", Freelancer.Freelancer_Description);
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
                    string strSql = "SELECT * FROM Freelancers ORDER BY Freelancer_ID;";
                    SqlCommand cmd = new SqlCommand(strSql, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SignUpFreelancerModel Freelancer = new SignUpFreelancerModel();
                        Freelancer.Freelancer_ID = Convert.ToInt32(rdr["Freelancer_ID"]);
                        Freelancer.Freelancer_Name = rdr["Freelancer_Name"].ToString();
                        Freelancer.Freelancer_Email = rdr["Freelancer_Email"].ToString();
                        Freelancer.Freelancer_PW = rdr["Freelancer_PW"].ToString();
                        Freelancer.Freelancer_Field = rdr["Freelancer_Field"].ToString();
                        Freelancer.Freelancer_Description = rdr["Freelancer_Description"].ToString();
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
                        Freelancer.Freelancer_Name = rdr["Freelancer_Name"].ToString();
                        Freelancer.Freelancer_Email = rdr["Freelancer_Email"].ToString();
                        Freelancer.Freelancer_PW = rdr["Freelancer_PW"].ToString();
                        Freelancer.Freelancer_Field = rdr["Freelancer_Field"].ToString();
                        Freelancer.Freelancer_Description = rdr["Freelancer_Description"].ToString();
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
                        strSQL = "UPDATE Freelancers SET Freelancer_Name = @Freelancer_Name, Freelancer_Email = @Freelancer_Email, Freelancer_PW = @Freelancer_PW, Freelancer_Field = @Freelancer_Field, Freelancer_Description = @Freelancer_Description, " + "Freelancer_Status = @Freelancer_Status WHERE Freelancer_ID = @Freelancer_ID;";
                    }
                    else
                    {
                        strSQL = "UPDATE Freelancers SET Freelancer_Name = @Freelancer_Name, Freelancer_Email = @Freelancer_Email, Freelancer_PW = @Freelancer_PW, Freelancer_Field = @Freelancer_Field, Freelancer_Description = @Freelancer_Description, " + "Freelancer_Status = @Freelancer_Status WHERE Freelancer_ID = @Freelancer_ID;";
                    }

                    cmd.CommandText = strSQL;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Freelancer_Name", tFreelancer.Freelancer_Name);
                    cmd.Parameters.AddWithValue("@Freelancer_Email", tFreelancer.Freelancer_Email);
                    cmd.Parameters.AddWithValue("@Freelancer_PW", tFreelancer.Freelancer_PW);
                    cmd.Parameters.AddWithValue("@Freelancer_Field", tFreelancer.Freelancer_Field);
                    cmd.Parameters.AddWithValue("@Freelancer_Description", tFreelancer.Freelancer_Description);
                    cmd.Parameters.AddWithValue("@Freelancer_Status", tFreelancer.Freelancer_Status);
                    cmd.Parameters.AddWithValue("@Freelancer_ID", tFreelancer.Freelancer_ID);
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
        public IEnumerable<SignUpFreelancerModel> GetFreelancerLogin(SignUpFreelancerModel tFreelancer)
        {
            List<SignUpFreelancerModel> lstFreelancer = new List<SignUpFreelancerModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT TOP 1 * FROM Freelancers WHERE Freelancer_Email = @Freelancer_Email AND Freelancer_PW = @Freelancer_PW;";

                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Freelancer_Email", tFreelancer.Freelancer_Email);
                    cmd.Parameters.AddWithValue("@Freelancer_PW", tFreelancer.Freelancer_PW);

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
