using Microsoft.Extensions.DependencyInjection;
using PeopleApp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Test.Data;

namespace Test.App
{
    class Program
    {
        public static int numberOfRequests = 100;
        public static PeopleDbContext _db;
        public static IList<User> Users;
        public static string Term;

        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddTransient<PeopleDbContext>();

            var provider = services.BuildServiceProvider();

            _db = provider.GetService<PeopleDbContext>();
            Users = _db.Users.ToList();
            Term = "jack";

            if (args != null)
            {
                if (args.Length > 1)
                {
                    numberOfRequests = int.Parse(args[1]);
                }

                if (args.Length > 2)
                {
                    Term = args[2];
                }

                switch (args[0])
                {
                    case "query":
                        TestToLowerQuery();
                        break;
                    case "byte":
                        TestByte();
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

        private static void TestToLowerQuery()
        {


            int count = 0;

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = _db.Users.Where(u => u.Givenname.ToLower() == Term).Count();
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
                count = Users.Where(u => u.Givenname.ToLower() == Term).Count();
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
                count = Users.Where(u => string.Equals(u.Givenname, Term, StringComparison.OrdinalIgnoreCase)).Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestBlaze()
        {
            List<byte[]> UsersByte = new List<byte[]>();
            foreach (var item in Users)
            {
                UsersByte.Add(Encoding.ASCII.GetBytes(item.Givenname.Trim()));
            }

            var jackAsByte = Encoding.ASCII.GetBytes(Term);


            int count = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;
                for (int j = 0; j < UsersByte.Count; j++)
                {

                    if ((UsersByte[j].SequenceEqual(jackAsByte)))
                    {
                        count++;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestByte()
        {
            List<byte[]> UsersByte = new List<byte[]>();
            foreach (var item in Users)
            {
                UsersByte.Add(Encoding.ASCII.GetBytes(item.Givenname.ToLower().Trim()));
            }
                       
            var jackAsByte = Encoding.ASCII.GetBytes(Term);


            int count = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;
                for (int j = 0; j < UsersByte.Count; j++)
                {

                    if ((UsersByte[j].SequenceEqual(jackAsByte)))
                    {
                        count++;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }
    }
}
