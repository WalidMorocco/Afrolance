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
    public class RegisterAdmin
    {
        [Required]
        public int Admin_ID { get; set; }

        [EmailAddress]
        [Display(Name = "Username")]
        public String Admin_Email { get; set; }

        [Required, StringLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String Admin_PW { get; set; }

        public String Feedback { get; set; }
    }
}
