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
    public class DashboardModel : PageModel
    {
        public RegisterFreelancer tFreelancer { get; set; }
        private readonly IConfiguration _configuration;

        public DashboardModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {

            IActionResult temp;
            if (HttpContext.Session.GetString("Freelancer_Email") is null)
            {
                temp = RedirectToPage("/Freelancer/Login");
            }
            else
            {
                temp = Page();
                ViewData["UserId"]=HttpContext.Session.GetInt32("Freelancer_ID");
                ViewData["UserEmail"] = HttpContext.Session.GetInt32("Freelancer_Email");

            }
            return temp;
        }

    }
}
