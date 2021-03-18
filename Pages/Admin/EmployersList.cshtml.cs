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
    public class EmployersListModel : PageModel
    {
        private readonly IConfiguration _configuration;
        SignUpEmployerDataAccessLayer factory;
        public List<SignUpEmployerModel> tix { get; set; }

        public EmployersListModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new SignUpEmployerDataAccessLayer(_configuration);
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
                tix = factory.GetActiveRecords().ToList();
                temp = Page();

            }
            return temp;
        }
    }
}
