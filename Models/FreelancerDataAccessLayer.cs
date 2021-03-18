// Function for the admin registration
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
    public class FreelancerDataAccessLayer
    {
        string connectionString;
        private readonly IConfiguration _configuration;
        public FreelancerDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");


        }
        public IEnumerable<RegisterFreelancer> GetFreelancerLogin(RegisterFreelancer tFreelancer)
        {
            List<RegisterFreelancer> lstFreelancer = new List<RegisterFreelancer>();

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
                        RegisterFreelancer tMatch = new RegisterFreelancer();
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
