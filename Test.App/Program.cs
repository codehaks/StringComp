﻿using Microsoft.Extensions.DependencyInjection;
using PeopleApp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Test.Data;

namespace Test.App
{
    class Program
    {
        public static int numberOfRequests = 100;

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<PeopleDbContext>();

            var provider = services.BuildServiceProvider();

            _db = provider.GetService<PeopleDbContext>();
            Users = _db.Users.ToList();

            if (args != null)
            {
                if (args.Length > 1)
                {
                    numberOfRequests = int.Parse(args[1]);
                }

                switch (args[0])
                {
                    case "query":
                        TestToLowerQuery();
                        break;

                    case "tolower":
                        TestToLower();
                        break;

                    case "equal":
                        TestEqual();
                        break;

                    default:
                        break;
                }



            }

        }

        private static PeopleDbContext _db;

        private static IList<User> Users;

        private static void TestToLowerQuery()
        {


            int count = 0;

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = _db.Users.Where(u => u.Givenname.ToLower() == "jack").Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestToLower()
        {


            int count = 0;

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {
                count = Users.Where(u => u.Givenname.ToLower() == "jack").Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestEqual()
        {
            int count = 0;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {
                count = Users.Where(u => string.Equals(u.Givenname, "jack", StringComparison.OrdinalIgnoreCase)).Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }
    }
}