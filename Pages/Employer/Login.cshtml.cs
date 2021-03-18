using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Afrolance.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Afrolance.Pages.Employer
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public RegisterEmployer tEmployer { get; set; }
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult OnPost()
        {
            IActionResult temp;
            List<RegisterEmployer> lstEmployer = new List<RegisterEmployer>();
            if (ModelState.IsValid == false)
            {
                temp = Page();
            }
            else
            {
                if (tEmployer != null)
                {
                    EmployerDataAccessLayer factory = new EmployerDataAccessLayer(_configuration);
                    lstEmployer = factory.GetEmployerLogin(tEmployer).ToList();

                    if (lstEmployer.Count > 0)
                    {
                        HttpContext.Session.SetInt32("Employer_ID", lstEmployer[0].Employer_ID);
                        HttpContext.Session.SetString("Employer_Email", lstEmployer[0].Employer_Email);
                        temp = Redirect("/Employer/Dashboard");
                        
                    }
                    else
                    {
                        tEmployer.Feedback = "Login Failed.";
                        temp = Page();
                    }
                }
                else
                {
                    temp = Page();
                }
            }

            return temp;

        }
    }
}
