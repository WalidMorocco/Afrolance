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
    public class AdminDataAccessLayer
    {
        string connectionString;
        private readonly IConfiguration _configuration;
        public AdminDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");


        }

        //Login Admin function
        public IEnumerable<RegisterAdmin> GetAdminLogin(RegisterAdmin tAdmin)
        {
            List<RegisterAdmin> lstAdmin = new List<RegisterAdmin>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strSQL = "SELECT TOP 1 * FROM AdminAccounts WHERE Admin_Email = @Admin_Email AND Admin_PW = @Admin_PW;";

                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Admin_Email", tAdmin.Admin_Email);
                    cmd.Parameters.AddWithValue("@Admin_PW", tAdmin.Admin_PW);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        RegisterAdmin tMatch = new RegisterAdmin();
                        tMatch.Admin_ID = Convert.ToInt32(rdr["Admin_ID"]);
                        tMatch.Admin_Email = rdr["Admin_Email"].ToString();
                        tMatch.Admin_PW = rdr["Admin_PW"].ToString();
                        lstAdmin.Add(tMatch);

                    }

                    con.Close();

                }
            }

            catch (Exception err)
            {

            }
            return lstAdmin;
        }
    }
}
