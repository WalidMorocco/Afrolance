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
        public String Freelancer_Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email.")]
        public String Freelancer_Email { get; set; }

        [Required, StringLength(255)]
        public String Freelancer_PW { get; set; }

        [Required]
        [StringOptionsValidate(Allowed = new String[] { "IT", "Design", "Music", "Cosmetic", "Photography", "Desk", "Security" },
            ErrorMessage = "Sorry, Category is invalid.  Categories: IT, Design, Music, Cosmetic, Photography, Desk, Security")]
        public String Freelancer_Field { get; set; }

        [Required]
        public String Freelancer_Description { get; set; }

        [Required]
        public bool Freelancer_Status { get; set; }

        public String Feedback { get; set; }
    }
}
