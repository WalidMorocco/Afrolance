// Function for the employer registration
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
    public class EmployerDataAccessLayer
    {
        string connectionString;
        private readonly IConfiguration _configuration;
        public EmployerDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");


        }
        public IEnumerable<RegisterEmployer> GetEmployerLogin(RegisterEmployer tEmployer)
        {
            List<RegisterEmployer> lstEmployer = new List<RegisterEmployer>();

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
                        RegisterEmployer tMatch = new RegisterEmployer();
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
