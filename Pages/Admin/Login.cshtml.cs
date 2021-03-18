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

namespace Afrolance.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public RegisterAdmin tAdmin { get; set; }
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
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
            List<RegisterAdmin> lstAdmin = new List<RegisterAdmin>();
            if (ModelState.IsValid == false)
            {
                temp = Page();
            }
            else
            {
                if (tAdmin != null)
                {
                    AdminDataAccessLayer factory = new AdminDataAccessLayer(_configuration);
                    lstAdmin = factory.GetAdminLogin(tAdmin).ToList();

                    if (lstAdmin.Count > 0)
                    {
                        HttpContext.Session.SetInt32("Admin_ID", lstAdmin[0].Admin_ID);
                        HttpContext.Session.SetString("Admin_Email", lstAdmin[0].Admin_Email);
                        temp = Redirect("/Admin/Dashboard");
                    }
                    else
                    {
                        tAdmin.Feedback = "Login Failed.";
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
