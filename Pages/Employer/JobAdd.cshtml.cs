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
    public class JobAddModel : PageModel
    {
        [BindProperty]
        public AddJobModel job { get; set; }
        AddJobDataAccessLayer factory;
        public List<AddJobDataAccessLayer> tix { get; set; }

        private readonly IConfiguration _configuration;

        public JobAddModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {

            IActionResult temp;
            if (HttpContext.Session.GetString("Employer_Email") is null)
            {
                temp = RedirectToPage("/Employer/Login");
            }
            else
            {
                temp = Page();
                ViewData["UserId"] = HttpContext.Session.GetInt32("Employer_ID");

            }
            return temp;
        }

        public IActionResult OnPost()
        {
            IActionResult temp;
            if (ModelState.IsValid == false)
            {
                temp = Page();
                ViewData["UserId"] = HttpContext.Session.GetInt32("Employer_ID");
            }
            else
            {
                if (!(job is null))
                {
                    AddJobDataAccessLayer factory = new AddJobDataAccessLayer(_configuration);
                    factory.Create(job);
                }
                temp = Page();
            }
            return temp;
        }
    }
}
