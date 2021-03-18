//Freelancer identification variables model
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
    public class RegisterFreelancer
    {
        [Required]
        public int Freelancer_ID { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email.")]
        public String Freelancer_Email { get; set; }

        [Required, StringLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String Freelancer_PW { get; set; }

        public String Feedback { get; set; }
    }
}
