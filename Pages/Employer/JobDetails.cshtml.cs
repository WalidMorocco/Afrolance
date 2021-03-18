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
    public class JobDetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public AddJobModel sue { get; set; }
        public AddJobDataAccessLayer factory;

        public JobDetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new AddJobDataAccessLayer(_configuration);
        }
        public ActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                sue = factory.GetOneRecord(id);
                HttpContext.Session.SetInt32("Job_ID", sue.Job_ID);
            }
            if (sue == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
