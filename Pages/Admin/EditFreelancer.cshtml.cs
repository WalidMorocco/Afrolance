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
    public class EditFreelancerModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public SignUpFreelancerModel sue { get; set; }
        public SignUpFreelancerDataAccessLayer factory;
        public List<SignUpFreelancerModel> tix { get; set; }

        public EditFreelancerModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new SignUpFreelancerDataAccessLayer(_configuration);
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

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            factory.UpdateFreelancer(sue);
            return RedirectToPage("/Admin/FreelancersList");
        }
    }
}
