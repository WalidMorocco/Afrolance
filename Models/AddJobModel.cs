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
    public class AddJobModel
    {
        [Required]
        public int Job_ID { get; set; } 

        [Required, StringLength(255)]
        public String Job_Title { get; set; } 

        [Required]
        public String Job_Desc { get; set; } 

        [Required]
        [StringOptionsValidate(Allowed = new String[] { "IT", "Design", "Music", "Cosmetic", "Photography", "Desk", "Security" },
            ErrorMessage = "Sorry, Category is invalid.  Categories: IT, Design, Music, Cosmetic, Photography, Desk, Security")]
        public String Category { get; set; } 

        [Required]
        [Display(Name = "Last time you've met the player in game")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        [MyDate(ErrorMessage = "Future date entry not allowed")]
        public DateTime Start_Date { get; set; }

        [Required]
        [Display(Name = "Last time you've met the player in game")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        [MyDate(ErrorMessage = "Future date entry not allowed")]
        public DateTime End_Date { get; set; }

        public String Employer_Notes { get; set; }

        [Required]
        [Range(1, 999999, ErrorMessage = "Amount too high.")]
        public int Pay { get; set; }

        public Boolean Active { get; set; }

        public String Feedback { get; set; }
    }
}
