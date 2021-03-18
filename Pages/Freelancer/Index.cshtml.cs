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
    public class IndexModel : PageModel
    {
        [BindProperty]
        public SignUpFreelancerModel tFreelancer { get; set; }
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            HttpContext.Session.SetInt32("test", 5);
        }

        public IActionResult OnPost()
        {
            IActionResult temp;
            List<SignUpFreelancerModel> lstFreelancer = new List<SignUpFreelancerModel>();
            if (ModelState.IsValid == false)
            {
                temp = Page();
            }
            else
            {
                if (tFreelancer != null)
                {
                    SignUpFreelancerDataAccessLayer factory = new SignUpFreelancerDataAccessLayer(_configuration);
                    lstFreelancer = factory.GetFreelancerLogin(tFreelancer).ToList();

                    if (lstFreelancer.Count > 0)
                    {
                        HttpContext.Session.SetInt32("Freelancer_ID", lstFreelancer[0].Freelancer_ID);
                        HttpContext.Session.SetString("Freelancer_Email", lstFreelancer[0].Freelancer_Email);
                        temp = Redirect("/Freelancer/ControlPanel");
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
