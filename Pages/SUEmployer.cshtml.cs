using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Afrolance.Models;
using Microsoft.Extensions.Configuration;


namespace Afrolance.Pages
{
    public class SUEmployerModel : PageModel
    {
        [BindProperty]
        public SignUpEmployerModel sue { get; set; }

        private readonly IConfiguration _configuration;

        public SUEmployerModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
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
                if (!(sue is null))
                {
                    SignUpEmployerDataAccessLayer factory = new SignUpEmployerDataAccessLayer(_configuration);
                    factory.Create(sue);
                }
                temp = Page();
            }
            return temp;
        }
    }
}
