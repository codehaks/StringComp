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

        private static void TestByte()
        {
            List<byte[]> UsersByte = new List<byte[]>();

            //var testUsers = new List<string>()
            //{
            //    "Jack","jack","Ali"
            //};

            //foreach (var item in testUsers)
            //{
            //    UsersByte.Add(Encoding.ASCII.GetBytes(item.ToLower().Trim()));
            //}



            foreach (var item in Users)
            {
                UsersByte.Add(Encoding.ASCII.GetBytes(item.Givenname.ToLower().Trim()));
            }



            var jackAsByte = Encoding.ASCII.GetBytes("jack");


            int count = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                //count = UsersByte.Where(u => u==jackAsByte).Count();
                for (int j = 0; j < UsersByte.Count; j++)
                {
                    //for (int k = 0; k < UsersByte[j].Length; k++)
                    //{

                    //}
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
