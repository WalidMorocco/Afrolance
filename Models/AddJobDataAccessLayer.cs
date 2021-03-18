//Job Database interaction functions
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
    public class AddJobDataAccessLayer
    {
        string connectionString;

        private readonly IConfiguration _configuration;

        public AddJobDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        //Add Job Function
        public void Create(AddJobModel job)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT Into Jobs (Employer_ID, Job_Title, Job_Desc, Category, Start_Date, End_Date, Employer_Notes, Pay) VALUES (@Employer_ID, @Job_Title, @Job_Desc, @Category, @Start_Date, @End_Date, @Employer_Notes, @Pay);";
                job.Feedback = "";

                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Employer_ID", job.Employer_ID);
                        command.Parameters.AddWithValue("@Job_Title", job.Job_Title);
                        command.Parameters.AddWithValue("@Job_Desc", job.Job_Desc);
                        command.Parameters.AddWithValue("@Category", job.Category);
                        command.Parameters.AddWithValue("@Start_Date", DateTime.Now);
                        command.Parameters.AddWithValue("@End_Date", DateTime.Now);
                        command.Parameters.AddWithValue("@Employer_Notes", job.Employer_Notes);
                        command.Parameters.AddWithValue("@Pay", job.Pay);
                        connection.Open();
                        job.Feedback = command.ExecuteNonQuery().ToString() + " Record Added";
                        connection.Close();
                    }
                }
                catch (Exception err)
                {
                    job.Feedback = "ERROR: " + err.Message;
                }

            }
        }

        //Job List Function
        public IEnumerable<AddJobModel> GetActiveRecords()
        {
            List<AddJobModel> lstTix = new List<AddJobModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT * FROM Jobs ORDER BY Start_Date";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        AddJobModel job = new AddJobModel();
                        job.Job_ID = Convert.ToInt32(rdr["Job_ID"]);
                        job.Employer_ID = Convert.ToInt32(rdr["Employer_ID"]);
                        job.Job_Title = rdr["Job_Title"].ToString();
                        job.Job_Desc = rdr["Job_Desc"].ToString();
                        job.Category = rdr["Category"].ToString();
                        job.Start_Date = DateTime.Parse(rdr["Start_Date"].ToString());
                        job.End_Date = DateTime.Parse(rdr["End_Date"].ToString());
                        job.Employer_Notes = rdr["Employer_Notes"].ToString();
                        job.Pay = Convert.ToInt32(rdr["Pay"]);

                        lstTix.Add(job);

                    }
                    con.Close();
                }
            }
            catch (Exception err)
            {

            }
            return lstTix;
        }

        //Job interaction function
        public AddJobModel GetOneRecord(int? id)
        {
            AddJobModel job = new AddJobModel();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT * FROM Jobs WHERE Job_ID = @Job_ID;";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Job_ID", id);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        job.Job_ID = Convert.ToInt32(rdr["Job_ID"]);
                        job.Employer_ID = Convert.ToInt32(rdr["Employer_ID"]);
                        job.Job_Title = rdr["Job_Title"].ToString();
                        job.Job_Desc = rdr["Job_Desc"].ToString();
                        job.Category = rdr["Category"].ToString();
                        job.Start_Date = DateTime.Parse(rdr["Start_Date"].ToString());
                        job.End_Date = DateTime.Parse(rdr["End_Date"].ToString());
                        job.Employer_Notes = rdr["Employer_Notes"].ToString();
                        job.Pay = Convert.ToInt32(rdr["Pay"]);
                    }
                    con.Close();
                }
            }

            catch (Exception err)
            {
                job.Feedback = "ERROR: " + err.Message;
            }

            return job;
        }

       // Update Job Data Funtction
        public void Updatejob(AddJobModel tjob)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    string strSQL;

                    if (tjob.Active == false)
                    {
                        strSQL = "UPDATE Jobs SET Job_Title = @Job_Title, Job_Desc = @Job_Desc, Category = @Category, Start_Date = @Start_Date, End_Date = @End_Date, Employer_Notes = @Employer_Notes, Pay = @Pay, " + "Active = @Active WHERE Job_ID = @Job_ID;";
                    }
                    else
                    {
                        strSQL = "UPDATE Jobs SET Job_Title = @Job_Title, Job_Desc = @Job_Desc, Category = @Category, Start_Date = @Start_Date, End_Date = @End_Date, Employer_Notes = @Employer_Notes, Pay = @Pay, " + "Active = @Active WHERE Job_ID = @Job_ID;";
                    }

                    cmd.CommandText = strSQL;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Job_ID", tjob.Job_ID);
                    cmd.Parameters.AddWithValue("@Job_Title", tjob.Job_Title);
                    cmd.Parameters.AddWithValue("@Job_Desc", tjob.Job_Desc);
                    cmd.Parameters.AddWithValue("@Category", tjob.Category);
                    cmd.Parameters.AddWithValue("@Start_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@End_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Pay", tjob.Pay);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            catch (Exception err)
            {
                tjob.Feedback = "ERROR: " + err.Message;
            }
        }

        //Deleting Job Data function
        public AddJobModel Deletejob(int? id)
        {
            AddJobModel job = new AddJobModel();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "DELETE FROM Jobs WHERE Job_ID = @Job_ID;";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Job_ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            catch (Exception err)
            {
                job.Feedback = "ERROR: " + err.Message;
            }

            return job;
        }
    }
}
