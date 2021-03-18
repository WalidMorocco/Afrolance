using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Afrolance.Models;
using Afrolance.Pages;
using Microsoft.Extensions.Configuration;

namespace Afrolance.Pages.Admin
{
    public class JobListModel : PageModel
    {
        private readonly IConfiguration _configuration;
        AddJobDataAccessLayer factory;
        public String FName { get; set; }
        public List<AddJobModel> tix { get; set; }

        public JobListModel(IConfiguration configuration)
        {
            _configuration = configuration;
            factory = new AddJobDataAccessLayer(_configuration);
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
