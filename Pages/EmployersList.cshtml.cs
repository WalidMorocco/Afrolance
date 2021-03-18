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
    public class EmployersListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public String FName { get; set; }

        private readonly IConfiguration _configuration;

        SignUpEmployerDataAccessLayer factory;
        public List<SignUpEmployerModel> tix { get; set; }


        public EmployersListModel(IConfiguration configuration)
        {
            //Constructor code
            _configuration = configuration;
            factory = new SignUpEmployerDataAccessLayer(_configuration);
        }


        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(FName))
            {
                FName = "You!";
            }

            //Fill in current empty list with records
            tix = factory.GetActiveRecords().ToList();

        }
    }
}
