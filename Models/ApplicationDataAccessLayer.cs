//Applciation Database interaction functions
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Afrolance.Models
{
    public class ApplicationDataAccessLayer
    {
        string connectionString;

        private readonly IConfiguration _configuration;

        public ApplicationDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public void Create(ApplicationModel apl)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT Into Applications (Job_ID, Freelancer_ID, Freelancer_Email, Job_Title) VALUES (@Job_ID, @Freelancer_ID, @Freelancer_Email, @Job_Title);";
                apl.Feedback = "";

                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Job_ID", apl.Job_ID);
                        command.Parameters.AddWithValue("@Freelancer_ID", apl.Freelancer_ID);
                        command.Parameters.AddWithValue("@Freelancer_Email", apl.Freelancer_Email);
                        command.Parameters.AddWithValue("@Job_Title", apl.Job_Title);
                        connection.Open();
                        apl.Feedback = command.ExecuteNonQuery().ToString() + " Record Added";
                        connection.Close();
                    }
                }
                catch (Exception err)
                {
                    apl.Feedback = "ERROR: " + err.Message;
                }

            }
        }

        public IEnumerable<ApplicationModel> GetActiveRecords()
        {
            List<ApplicationModel> lstTix = new List<ApplicationModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT * FROM Applications ORDER BY Freelancer_Email";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ApplicationModel apl = new ApplicationModel();
                        apl.Application_ID = Convert.ToInt32(rdr["Application_ID"]);
                        apl.Job_ID = Convert.ToInt32(rdr["Job_ID"]);
                        apl.Freelancer_ID = Convert.ToInt32(rdr["Freelancer_ID"]);
                        apl.Freelancer_Email = rdr["Freelancer_Email"].ToString();
                        apl.Job_Title = rdr["Job_Title"].ToString();
                        lstTix.Add(apl);

                    }
                    con.Close();
                }
            }
            catch (Exception err)
            {

            }
            return lstTix;
        }

        public ApplicationModel GetOneRecord(int? id)
        {
            ApplicationModel apl = new ApplicationModel();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT * FROM Applications WHERE Application_ID = @Application_ID;";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Application_ID", id);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        apl.Application_ID = Convert.ToInt32(rdr["Application_ID"]);
                        apl.Job_ID = Convert.ToInt32(rdr["Job_ID"]);
                        apl.Freelancer_ID = Convert.ToInt32(rdr["Freelancer_ID"]);
                        apl.Freelancer_Email = rdr["Freelancer_Email"].ToString();
                        apl.Job_Title = rdr["Job_Title"].ToString();
                    }
                    con.Close();
                }
            }

            catch (Exception err)
            {
                apl.Feedback = "ERROR: " + err.Message;
            }

            return apl;
        }
    }
}
