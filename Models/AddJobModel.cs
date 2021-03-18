// Job model Variables Database
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
        public int Job_ID { get; set; }  //Job ID

        [Required]
        public int Employer_ID { get; set; } //Job Employer's ID

        [Required, StringLength(255)]
        public String Job_Title { get; set; }  // Chosen name for the job

        [Required]
        public String Job_Desc { get; set; }  //Desctiption of the job

        [Required]
        [StringOptionsValidate(Allowed = new String[] { "IT", "Marketing", "Design", "Business", "Communication", "Medical", "Electrical", "Architecture", "Languages", "Driving", "Teaching", "Music", "Cosmetic", "Photography", "Desk", "Security" },
            ErrorMessage = "Sorry, Category is invalid.")]
        public String Category { get; set; }  //Field of job

        [Required]
        [Display(Name = "Starting date and time of the job")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        [MyDate(ErrorMessage = "Future date entry not allowed")]
        public DateTime Start_Date { get; set; } //Time staritng the job

        [Required]
        [Display(Name = "Ending date and time of the job")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        [MyDate(ErrorMessage = "Future date entry not allowed")]
        public DateTime End_Date { get; set; } //Time ending the job

        public String Employer_Notes { get; set; } //Notes aside the job

        [Required]
        [Range(1, 99999999999999, ErrorMessage = "Amount too high.")]
        public int Pay { get; set; } //Total Pay for the job

        public Boolean Active { get; set; } //Account status

        public String Feedback { get; set; }
    }
}
