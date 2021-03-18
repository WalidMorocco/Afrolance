using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Afrolance.Models;
using Afrolance.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Afrolance.Pages.Admin
{
    public class PostedJobListModel : PageModel
    {
        private readonly IConfiguration _configuration;
        AddJobDataAccessLayer factory;
        public RegisterAdmin tAdmin { get; set; }
        public List<AddJobModel> tix { get; set; }

        public PostedJobListModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new AddJobDataAccessLayer(_configuration);
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
