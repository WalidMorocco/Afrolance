using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Afrolance.Models;
using Microsoft.Extensions.Configuration;

namespace Afrolance.Pages.Employer
{
    public class JobAddModel : PageModel
    {
        [BindProperty]
        public AddJobModel job { get; set; }

        private readonly IConfiguration _configuration;

        public JobAddModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            IActionResult temp;
            if (ModelState.IsValid == false)
            {
                temp = Page();
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
