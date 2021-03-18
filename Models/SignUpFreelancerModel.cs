// Freelancer model Variables Database
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Afrolance.Models
{
    public class SignUpFreelancerModel
    {
        [Required]
        public int Freelancer_ID { get; set; }

        [Required, StringLength(255)]
        public String Freelancer_FName { get; set; }

        [Required, StringLength(255)]
        public String Freelancer_LName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email.")]
        public String Freelancer_Email { get; set; }

        [Required, StringLength(255)]
        [MinLength(8)]
        public String Freelancer_PW { get; set; }

        [Required]
        [StringOptionsValidate(Allowed = new String[] { "IT", "Marketing", "Design", "Business", "Communication", "Medical", "Electrical", "Architecture", "Languages", "Driving", "Teaching", "Music", "Cosmetic", "Photography", "Desk", "Security" },
            ErrorMessage = "Sorry, Category is invalid.")]
        public String Freelancer_Field { get; set; }

        [Required]
        public String Freelancer_Resume { get; set; }

        public String Freelancer_Bio { get; set; }

        [Required]
        public String Freelancer_Phone { get; set; }

        [Required]
        public String Freelancer_Country { get; set; }

        [Required]
        public String Freelancer_City { get; set; }
        
        public bool Freelancer_Status { get; set; }

        public String Feedback { get; set; }
    }
}
