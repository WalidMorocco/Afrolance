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
    public class ApplicationPageModel : PageModel
    {
        [BindProperty]
        public ApplicationModel apl { get; set; }
        ApplicationDataAccessLayer factory;
        public List<ApplicationDataAccessLayer> tix { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationPageModel(IConfiguration configuration)
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
                ViewData["JobId"] = HttpContext.Session.GetInt32("JobId");
                ViewData["UserId"] = HttpContext.Session.GetInt32("Freelancer_ID");
                ViewData["UserEmail"] = HttpContext.Session.GetString("Freelancer_Email");
                ViewData["JobTitle"] = HttpContext.Session.GetString("JobTitle");

            }
            return temp;
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
                if (!(apl is null))
                {
                    ApplicationDataAccessLayer factory = new ApplicationDataAccessLayer(_configuration);
                    factory.Create(apl);
                }
                temp = Redirect("/Freelancer/JobList");
            }
            return temp;
        }
    }
}
