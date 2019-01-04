using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Test.App
{
    public static class TestVector
    {
        public static void Execute(int numberOfRequests,IList<string> users,string term)
        {
            int count = 0;

            List<BitArray> UsersByte = new List<BitArray>();
            foreach (var item in users)
            {
                var bits = new BitArray(Encoding.ASCII.GetBytes(item.Trim()));
                UsersByte.Add(bits);
            }

            var termAsByte = Encoding.ASCII.GetBytes(term.ToLower());
            var termBits = new BitArray(termAsByte);

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < numberOfRequests; i++)
            {
                count = 0;
                for (int j = 0; j < UsersByte.Count; j++)
                {

                    if (termBits[0] != UsersByte[j][0])
                    {
                        continue;
                    }
                    else
                    if (termBits.Length != UsersByte[j].Length)
                    {
                        continue;
                    }
                    else
                    if (CompareBites(termBits, UsersByte[j]))
                    //if (IsSame(termBits.Xor(UsersByte[j])))
                    {
                        count++;
                    }
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
    }
}
