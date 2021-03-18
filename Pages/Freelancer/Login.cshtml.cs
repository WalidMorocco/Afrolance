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

namespace Afrolance.Pages.Freelancer
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public RegisterFreelancer tFreelancer { get; set; }
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult OnPost()
        {
            IActionResult temp;
            List<RegisterFreelancer> lstFreelancer = new List<RegisterFreelancer>();
            if (ModelState.IsValid == false)
            {
                temp = Page();
            }
            else
            {
                if (tFreelancer != null)
                {
                    FreelancerDataAccessLayer factory = new FreelancerDataAccessLayer(_configuration);
                    lstFreelancer = factory.GetFreelancerLogin(tFreelancer).ToList();

                    if (lstFreelancer.Count > 0)
                    {
                        HttpContext.Session.SetInt32("Freelancer_ID", lstFreelancer[0].Freelancer_ID);
                        HttpContext.Session.SetString("Freelancer_Email", lstFreelancer[0].Freelancer_Email);
                        temp = Redirect("/Freelancer/Dashboard");

                        
                    }
                    else
                    {
                        tFreelancer.Feedback = "Login Failed.";
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
