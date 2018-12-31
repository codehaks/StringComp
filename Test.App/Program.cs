using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Test.Data;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<PeopleDbContext>();

            var provider = services.BuildServiceProvider();

            using (var db = provider.GetService<PeopleDbContext>())
            {
                Console.WriteLine(db.Users.Count());
            }


            
        }
    }
}
