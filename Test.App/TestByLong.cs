using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Test.App
{
    public static class TestByLong
    {
        public static void Execute(int numberOfRequests, IList<string> users, string term)
        {
            List<byte[]> usersAsByte = new List<byte[]>();
            List<int> usersAsLong = new List<int>();
            foreach (var item in users)
            {
                usersAsByte.Add(Encoding.ASCII.GetBytes(item.Trim()));
                var val = Encoding.ASCII.GetBytes(item.Trim());
                if (val.Length==4)
                {
                    //Console.WriteLine(item);
                    usersAsLong.Add(BitConverter.ToInt32(val));
                }
                
            }

            var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());
            var termAsInt = BitConverter.ToInt32(termAsByte);
            var count = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;
                for (int j = 0; j < usersAsLong.Count; j++)
                {
                    if (usersAsLong[j] == termAsInt)
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
