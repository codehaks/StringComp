using Microsoft.Extensions.DependencyInjection;
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
        
        public static string Term;

        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddTransient<PeopleDbContext>();

            var provider = services.BuildServiceProvider();

            _db = provider.GetService<PeopleDbContext>();
            var users = _db.Users.Select(u => u.Givenname.ToLower().Trim()).ToList();
            Term = "Jack";

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
                    case "blaze":
                        TestBlaze(users);
                        break;

                    case "byte":
                        TestByte(users);
                        break;

                    case "tolower":
                        TestToLower(users);
                        break;
                    case "tolower2":
                        TestToLower2(users);
                        break;

                    case "equal":
                        TestEqual(users);
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
                count = _db.Users.Where(u => u.Givenname.ToLower() == Term.ToLower()).Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestToLower(IList<string> users)
        {


            int count = 0;

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {
                count = users.Where(u => u.ToLower() == Term.ToLower()).Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestToLower2(IList<string> users)
        {


            int count = 0;

            var sw = Stopwatch.StartNew();
            var term = Term.ToLower();
            for (int i = 0; i < numberOfRequests; i++)
            {
                count = users.Where(u => u.ToLower() ==term).Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestEqual(IList<string> users)
        {
            int count = 0;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {
                count = users.Where(u => string.Equals(u, Term, StringComparison.OrdinalIgnoreCase)).Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestBlaze(IList<string> users)
        {
            List<byte[]> UsersByte = new List<byte[]>();
            foreach (var item in users)
            {
                UsersByte.Add(Encoding.ASCII.GetBytes(item.Trim()));
            }

            var jackAsByte = Encoding.ASCII.GetBytes(Term.ToLower());


            int count = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;
                for (int j = 0; j < UsersByte.Count; j++)
                {
                    if (UsersByte[j][0] != jackAsByte[0])
                    {
                        continue;
                    }
                    if ((UsersByte[j].SequenceEqual(jackAsByte)))
                    {
                        count++;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void TestByte(IList<string> users)
        {
            List<byte[]> UsersByte = new List<byte[]>();
            foreach (var item in users)
            {
                UsersByte.Add(Encoding.ASCII.GetBytes(item.ToLower().Trim()));
            }

            var termAsByte = Encoding.ASCII.GetBytes(Term.ToLower());


            int count = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;
                for (int j = 0; j < UsersByte.Count; j++)
                {

                    if ((UsersByte[j].SequenceEqual(termAsByte)))
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
