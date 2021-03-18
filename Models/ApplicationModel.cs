// Application model Database
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
    public class ApplicationModel
    {
        [Required]
        public int Application_ID { get; set; }

        [Required]
        [Key]
        public int Job_ID { get; set; }

        [Required]
        public int Freelancer_ID { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [Display(Name = "Username")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email.")]
        public String Freelancer_Email { get; set; }

        public String Job_Title { get; set; }

        public String Feedback { get; set; }
    }
}
