using Microsoft.AspNetCore.Mvc;
using PeopleApp.Common;
using PeopleApp.Data;
using System;
using System.Linq;

namespace PeopleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeopleDbContext _db;
        public HomeController(PeopleDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var model = _db.Users.Take(10).Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        [Route("user/order/age")]
        public IActionResult OrderByAge()
        {
            var model = _db.Users.OrderByAge(true).Take(10).Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        [Route("user/create")]
        public IActionResult Create()
        {
            var user=_db.Users.CreateByName("Hakim");
            _db.SaveChanges();
            return Ok(user.Number);
        }

        public IActionResult ToLower()
        {

            var all = _db.Users.ToList();
            var now = DateTime.Now;
            int jacksCount = 0;

            for (int i = 0; i < 1000; i++)
            {
                jacksCount = all.Where(u => u.Givenname.ToLower() == "jack").Count();
            }

            var duration = (DateTime.Now - now).TotalSeconds;
            return Ok(jacksCount.ToString() + " , Time : " + duration);
        }



        public IActionResult Equal()
        {

            var all = _db.Users.ToList();
            var now = DateTime.Now;
            int jacksCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                jacksCount = all.Where(u => string.Equals(u.Givenname, "jack", StringComparison.OrdinalIgnoreCase)).Count();
            }

            var duration = (DateTime.Now - now).TotalSeconds;

            return Ok(jacksCount + " , Time : " + duration);
        }


    }
}