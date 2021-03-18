using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Afrolance.Models
{
    public class SignUpEmployerModel
    {
        [Required]
        public int Employer_ID { get; set; }

        [Required, StringLength(255)]
        public String Employer_Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email.")]
        public String Employer_Email { get; set; }

        [Required, StringLength(255)]
        public String Employer_PW { get; set; }

        [Required]
        [StringOptionsValidate(Allowed = new String[] { "IT", "Design", "Music", "Cosmetic", "Photography", "Desk", "Security" },
            ErrorMessage = "Sorry, Category is invalid.  Categories: IT, Design, Music, Cosmetic, Photography, Desk, Security")]
        public String Employer_Field { get; set; }

        [Required]
        public String Employer_Description { get; set; }

        [Required]
        public bool Employer_Status { get; set; }

        public String Feedback { get; set; }
    }
}
