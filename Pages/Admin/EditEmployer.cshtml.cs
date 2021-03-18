using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Afrolance.Models;
using Microsoft.Extensions.Configuration;

namespace Afrolance.Pages.Admin
{
    public class EditEmployerModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public SignUpEmployerModel sue { get; set; }
        public SignUpEmployerDataAccessLayer factory;

        public EditEmployerModel(IConfiguration configuration)
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
        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            factory.UpdateEmployer(sue);
            return RedirectToPage("/Admin/ControlPanel");
        }
    }
}
