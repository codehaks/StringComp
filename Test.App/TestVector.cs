using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Test.App
{
    public static class TestVector
    {
        public static void Execute(int numberOfRequests, IList<string> users, string term)
        {
            int count = 0;

            List<BitArray> usersAsBits = new List<BitArray>();
            foreach (var item in users)
            {
                var bits = new BitArray(Encoding.ASCII.GetBytes(item.Trim()));
                usersAsBits.Add(bits);
            }

            var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());
            var termAsBits = new BitArray(termAsByte);
            var totalCount = usersAsBits.Count;
            var rem = totalCount % 8;
            var rounds = totalCount / 8;

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;


                for (int j = 0; j < usersAsBits.Count; j++)
                {
                    if (termAsBits.Length != usersAsBits[j].Length)
                    {
                        continue;
                    }

                    //if (termAsBits[0] != usersAsBits[j][0])
                    //{
                    //    continue;
                    //}
                    //else
                    //if (termAsBits.Length != usersAsBits[j].Length)
                    //{
                    //    continue;
                    //}
                    //else
                    if (CompareBites(termAsBits, usersAsBits[j]))

                    {
                        count++;
                    }

                    //count++;
                }
            }

            sw.Stop();
            Console.WriteLine($"Found : {count} => Time : {sw.ElapsedMilliseconds,-3:N0}");
        }


        private static bool IsSame(BitArray bits)
        {

            for (int i = 0; i < bits.Length; i++)
            {
                //Console.WriteLine(bits[i]);
                if (bits[i] == true)
                {
                    //  Console.WriteLine(i);
                    return false;
                }
            }
            return true;
        }
        private static bool CompareBites(BitArray bit1, BitArray bit2)
        {
            for (int i = 0; i < bit1.Length; i++)
            {
                if (bit1[i] != bit2[i])
                {
                    return false;
                }
            }

            return true;
        }


        public static void Execute1(int numberOfRequests, IList<string> users, string term)
        {
            int count = 0;

            List<BitArray> usersAsBits = new List<BitArray>();
            foreach (var item in users)
            {
                var bits = new BitArray(Encoding.ASCII.GetBytes(item.Trim()));
                usersAsBits.Add(bits);
            }

            var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());
            var termAsBits = new BitArray(termAsByte);
            var totalCount = usersAsBits.Count;
            var rem = totalCount % 8;
            var rounds = totalCount / 8;

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;


                for (int j = 0; j < usersAsBits.Count; j++)
                {
                    if (termAsBits.Length != usersAsBits[j].Length)
                    {
                        continue;
                    }
                    if (CompareBites(termAsBits, usersAsBits[j]))

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
