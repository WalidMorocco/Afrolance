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
    public class DeleteJobModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public AddJobModel sue { get; set; }
        public AddJobDataAccessLayer factory;

        public DeleteJobModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new AddJobDataAccessLayer(_configuration);
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
            factory.Deletejob(id);
            return RedirectToPage("/Admin/ControlPanel");
        }
    }
}
