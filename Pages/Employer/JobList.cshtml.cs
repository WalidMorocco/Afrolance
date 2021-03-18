using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

using Afrolance.Models;

using Microsoft.Extensions.Configuration;

namespace Afrolance.Pages.Employer
{
    public class JobListModel : PageModel
    {
        private readonly IConfiguration _configuration;
        AddJobDataAccessLayer factory;
        public SignUpEmployerModel tEmployer { get; set; }
        public List<AddJobModel> tix { get; set; }

        public JobListModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new AddJobDataAccessLayer(_configuration);
        }

        public void OnGet()
        {
            tix = factory.GetActiveRecords().ToList();
        }

        public void OnPost()
        {
            HttpContext.Session.SetInt32("Job_ID", tix[0].Job_ID);
            Redirect("/Employer/Dashboard");
        }

    }
}
