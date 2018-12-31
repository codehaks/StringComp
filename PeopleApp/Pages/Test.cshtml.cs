using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PeopleApp.Data;

namespace PeopleApp.Pages
{
    public class TestModel : PageModel
    {
        public IList<User> Output { get; set; }
        public void OnGet([FromServices] PeopleDbContext db)
        {
            Output = db.Users.Take(35000).ToList();
        }
    }
}