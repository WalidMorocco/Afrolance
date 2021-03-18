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

namespace Afrolance.Pages.Freelancer
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

        public ActionResult OnPost(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            HttpContext.Session.SetInt32("JobId", tix[0].Job_ID);
            HttpContext.Session.SetString("JobTitle", tix[0].Job_Title);
            return RedirectToPage("/Freelancer/JobDetails");
        }
    }
}
