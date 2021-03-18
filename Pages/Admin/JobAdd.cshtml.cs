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

namespace Afrolance.Pages.Admin
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
