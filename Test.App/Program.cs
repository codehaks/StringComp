﻿using Microsoft.Extensions.DependencyInjection;
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
            Term = "Jack".ToLower();

            if (args != null)
            {
                if (args.Length > 1)
                {
                    numberOfRequests = int.Parse(args[1]);
                }

                if (args.Length > 2)
                {
                    Term = args[2];
                    if (Term.Length<3)
                    {
                        Console.WriteLine("Too short!");
                        return;
                    }
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
                    case "equal2":
                        TestEqual2(users);
                        break;
                    case "equal3":
                        TestEqual3(users);
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
                count = _db.Users.Count(u => u.Givenname.ToLower() == Term.ToLower());
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
            var term = Term.ToLower();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {
                count = users.Where(u => u.ToLower() == term).Count();
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
        private static void TestEqual2(IList<string> users)
        {
            int count = 0;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {

                count = users.Where(u => char.Equals(u[0], Term[0]) && string.Equals(u, Term, StringComparison.OrdinalIgnoreCase)).Count();
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }
        private static void TestEqual3(IList<string> users)
        {
            int count = 0;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;

                for (int j = 0; j < users.Count; j++)
                {

                    if (!Equals(users[j][0], Term[0]))
                    {
                        continue;
                    }
                    else if (string.Equals(users[j], Term))
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
            ConvertStringToByteArray(users, out List<byte[]> UsersByte, out byte[] termAsByte);

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
                    if (UsersByte[j][0] != jackAsByte[0]
                        || UsersByte[j][1] != jackAsByte[1]
                        || UsersByte[j][2] != jackAsByte[2]
                        || UsersByte[j][3] != jackAsByte[3]
                        )
                    {
                        continue;
                    }
                    else
                    if ((UsersByte[j].SequenceEqual(jackAsByte)))
                    {
                        count++;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }

        private static void ConvertStringToByteArray(IList<string> users, out List<byte[]> UsersByte, out byte[] termAsByte)
        {
            UsersByte = new List<byte[]>();
            foreach (var item in users)
            {
                UsersByte.Add(Encoding.ASCII.GetBytes(item.ToLower().Trim()));
            }

            termAsByte = Encoding.ASCII.GetBytes(Term.ToLower());
        }
    }
}
