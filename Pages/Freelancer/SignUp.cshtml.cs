using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Afrolance.Models;
using Microsoft.Extensions.Configuration;

namespace Afrolance.Pages.Freelancer
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public SignUpFreelancerModel suf { get; set; }

        private readonly IConfiguration _configuration;

        public SignUpModel(IConfiguration configuration)
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
                if (!(suf is null))
                {
                    SignUpFreelancerDataAccessLayer factory = new SignUpFreelancerDataAccessLayer(_configuration);
                    factory.Create(suf);
                }
                temp = Page();
            }
            return temp;
        }
    }
}
