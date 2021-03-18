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

namespace Afrolance.Pages.Employer
{
    public class EmployersPageModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public SignUpEmployerModel sue { get; set; }
        public SignUpEmployerDataAccessLayer factory;
        public List<SignUpEmployerModel> tix { get; set; }

        public EmployersPageModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new SignUpEmployerDataAccessLayer(_configuration);
        }
        public ActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                sue = factory.GetOneRecord(id);
            }
            if (sue == null)
            {
                return NotFound();
            }
            return Page();
        }
        public ActionResult OnPost(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            factory.DeleteEmployer(id);
            return RedirectToPage("/Admin/ControlPanel");
        }
    }
}
