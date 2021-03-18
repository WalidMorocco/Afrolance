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
            if (HttpContext.Session.GetString("Admin_Email") is null)
            {
                temp = RedirectToPage("/Admin/Login");
            }
            else
            {
                temp = Page();

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
                temp = Redirect("/Admin/JobList");
            }
            return temp;
        }
    }
}
